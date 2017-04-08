using System;
using System.Net.Sockets;
using SteamKit2;

namespace SmokeyLib
{
    public class SteamAccount
    {
        private const string SENTRYFILEPATH = "sentry.bin";

        private CallbackManager _callbackManager;
        private SteamClient _client;
        private SteamUser _user;

        private string _username;
        private string _password;
        private string _twoFaCode;
        private string _guardCode;
        private bool _rememberPw;

        private EventManager _eventManager;

        private bool _expectReconnect = false;

        public SteamAccount(EventManager mgr)
        {
            _eventManager = mgr ?? throw new ArgumentException("EventManager can't be null !");

            SteamDirectory.Initialize();
            
            _client = new SteamClient(ProtocolType.Tcp);
            _user = _client.GetHandler<SteamUser>();
            _callbackManager = new CallbackManager(_client);

            _client.AddHandler(_user);

            _callbackManager.Subscribe<SteamClient.ConnectedCallback>(OnConnectedCallback);
            _callbackManager.Subscribe<SteamClient.DisconnectedCallback>(OnDisconnectedCallback);
            _callbackManager.Subscribe<SteamUser.LoggedOnCallback>(OnLoggedOnCallback);
            _callbackManager.Subscribe<SteamUser.LoginKeyCallback>(OnLoginKeyCallback);
            _callbackManager.Subscribe<SteamUser.UpdateMachineAuthCallback>(OnUpdateMachineAuthCallback);
        }

        public void DoLogin(string username, string password, string twoFaCode, string guardCode, bool rememberPw)
        {
            _username = username;
            _password = password;
            _twoFaCode = twoFaCode;
            _guardCode = guardCode;
            _rememberPw = rememberPw;

            _client.Connect();
        }

        private void OnLoginKeyCallback(SteamUser.LoginKeyCallback callback)
        {
            Properties.Settings.Default.SessionKey = _rememberPw ? callback.LoginKey : null;
            Properties.Settings.Default.Username = _rememberPw ? _username : null;
        }

        private void OnConnectedCallback(SteamClient.ConnectedCallback callback)
        {
            _user.LogOn(new SteamUser.LogOnDetails
            {
                Username = _username,
                Password = _password,
                TwoFactorCode = _twoFaCode,
                AuthCode = _guardCode,
                ShouldRememberPassword = _rememberPw,
                SentryFileHash = Convert.FromBase64String(Properties.Settings.Default.SentryHash),
                LoginKey = Properties.Settings.Default.SessionKey
            });
        }

        private void OnLoggedOnCallback(SteamUser.LoggedOnCallback callback)
        {
            if(callback.Result != EResult.OK)
            {
                _eventManager.Handle(new SteamEvent.LoginFailed { Reason = callback.Result });
                return;
            }

            _eventManager.Handle(new SteamEvent.LoggedIn { } );
        }

        private void OnUpdateMachineAuthCallback(SteamUser.UpdateMachineAuthCallback callback)
        {
            var hash = CryptoHelper.SHAHash(callback.Data);
            Properties.Settings.Default.SentryHash = Convert.ToBase64String(hash);

            _user.SendMachineAuthResponse(new SteamUser.MachineAuthDetails
            {
                BytesWritten = callback.BytesToWrite,
                FileName = callback.FileName,
                FileSize = callback.Data.Length,
                JobID = callback.JobID,
                LastError = 0,
                Offset = callback.Offset,
                OneTimePassword = callback.OneTimePassword,
                Result = EResult.OK,
                SentryFileHash = hash
            });

            _expectReconnect = true;
        }

        private void OnDisconnectedCallback(SteamClient.DisconnectedCallback callback)
        {
            if (!_expectReconnect)
            {
                _eventManager.Handle(new SteamEvent.Disconnected { CausedByUser = callback.UserInitiated });
                return;
            }

            _expectReconnect = false;
            _client.Connect();
        }
    }
}