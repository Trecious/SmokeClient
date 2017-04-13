using System;
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

            account.DoLogin("123", "456", null, null, false);

            Console.ReadKey();
        }

        private static void OnLoginFailed(SteamEvent.LoginFailed obj)
        {
            Console.WriteLine("Login Failed: " + obj.Reason);
        }

        private static void OnLoggedIn(SteamEvent.LoggedIn obj)
        {
            Console.WriteLine("Logged in !");
        }
    }
}
