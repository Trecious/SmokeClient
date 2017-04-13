using SteamKit2;
using System;

namespace SmokeyLib
{
    public class SteamEvent
    {
        public class Disconnected : IEventMsg
        {
            public bool CausedByUser { get; set; }
        }

        public class LoginFailed : IEventMsg
        {
            public enum EReason
            {
                InvalidPassword = 5,
                LoggedInElsewhere = 6,
                AccountLogonDenied = 63,
                AccountDisabled = 43,
                PasswordUnset = 56,
                AccountLogonDeniedNoMail = 66,
                AccountLockedDown = 73,
                AccountLogonDeniedVerifiedEmailRequired = 74,
                AccountLoginDeniedNeedTwoFactor,
                AccountLoginDeniedThrottle = 87,
                AccountLimitExceeded = 95,
                AccountActivityLimitExceeded = 96
            }

            public EReason Reason { get; set; }
        }

        public class LoggedIn : IEventMsg
        {
        }
    }
}
