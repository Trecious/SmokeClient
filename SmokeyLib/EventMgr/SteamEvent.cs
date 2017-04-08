using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteamKit2;

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
            public EResult Reason { get; set; }
        }

        public class LoggedIn : IEventMsg
        {

        }
    }
}
