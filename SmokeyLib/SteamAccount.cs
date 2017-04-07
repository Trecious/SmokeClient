using System;
using System.Net.Sockets;
using SteamKit2;

namespace SmokeyLib
{
    class SteamAccount
    {
        private const string SENTRYFILEPATH = "sentry.bin";

        private CallbackManager _callbackManager;
        private SteamClient _client;
        private SteamUser _user;

        private string _username;
        private string _password;

        private EventManager _eventManager;

        public SteamAccount(EventManager mgr)
        {
            _eventManager = mgr ?? throw new ArgumentException("EventManager can't be null !");

            SteamDirectory.Initialize();
            
            _client = new SteamClient(ProtocolType.Tcp);
            _callbackManager = new CallbackManager(_client);

            _client.AddHandler(_user);

            _callbackManager.Subscribe<SteamClient.ConnectedCallback>(OnConnectedCallback);
            _callbackManager.Subscribe<SteamClient.DisconnectedCallback>(OnDisconnectedCallback);
            _callbackManager.Subscribe<SteamUser.LoggedOnCallback>(OnLoggedOnCallback);
            _callbackManager.Subscribe<SteamUser.UpdateMachineAuthCallback>(OnUpdateMachineAuthCallback);
        }

        public void DoLogin(string username, string password, string twoFaCode, string steamGuardCode)
        {
            throw new NotImplementedException();
        }

        private void OnConnectedCallback(SteamClient.ConnectedCallback callback)
        {
            throw new NotImplementedException();
        }

        private void OnLoggedOnCallback(SteamUser.LoggedOnCallback callback)
        {
            throw new NotImplementedException();
        }

        private void OnUpdateMachineAuthCallback(SteamUser.UpdateMachineAuthCallback callback)
        {
            throw new NotImplementedException();
        }

        private void OnDisconnectedCallback(SteamClient.DisconnectedCallback callback)
        {
            throw new NotImplementedException();
        }
    }
}
