using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SomkeClient.BackEnd
{
    class GetCmListResponse
    {
        
    }

    class Server
    {
        public static string[] GetCmServers(int cellId)
        {
            var data = new Dictionary<string, string>
            {
                { "cellid", cellId.ToString() }
            };

            var result = WebAPI.Call(HttpMethod.Get, "ISteamDirectory", "GetCMList", 1, data, string.Empty, true);

            try
            {
                return JsonConvert.DeserializeObject<SomkeClient.GetCmListResponse>(result).Content.Serverlist;
            }
            catch (JsonException e) { }

            return null;
        }
    }
}
