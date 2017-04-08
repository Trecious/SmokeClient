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

            account.DoLogin("do not even", "think", "about", "it :)", false);
        }

        private static void OnLoggedIn(SteamEvent.LoggedIn obj)
        {
            
        }
    }
}
