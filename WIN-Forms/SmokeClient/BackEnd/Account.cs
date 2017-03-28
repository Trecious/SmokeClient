using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomkeClient;


namespace SomkeClient
{
    public sealed class Test : IEventMsg
    {
        public string Type { get; set; }
    }

    class Account
    {
        private string _password;
        private string _username;


        public Account(string username)
        {
            var test = new EventManager();
            test.Subscribe<Test>(null);
        }
    }
}
