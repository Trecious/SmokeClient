using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmokeyLib;

namespace SmokeyTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventManager = new EventManager();
            var account = new SteamAccount(eventManager);

            eventManager.Subscribe<SteamEvent.LoggedIn>(OnLoggedIn);
            eventManager.Subscribe<SteamEvent.LoginFailed>(OnLoginFailed);

            account.DoLogin(null, null, null, null, false);
            Console.ReadKey();
        }

        private static void OnLoginFailed(SteamEvent.LoginFailed obj)
        {
            Console.ReadKey();
        }

        private static void OnLoggedIn(SteamEvent.LoggedIn obj)
        {
            Console.WriteLine("OK");
            Console.ReadKey();
        }
    }
}
