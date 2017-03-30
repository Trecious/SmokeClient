using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace SomkeClient.BackEnd
{
    class ServerProvider
    {
        public static List<IPEndPoint> GetCmServers(int cellId)
        {
            var data = new Dictionary<string, string>
            {
                { "cellid", cellId.ToString() }
            };

            var result = WebAPI.Call(HttpMethod.Get, "ISteamDirectory", "GetCMList", 1, data, string.Empty, true);

            try
            {
                var servers = JsonConvert.DeserializeObject<GetCmListResponse>(result).Content.Serverlist;
                return servers.Select(StringToIpEndpoint).Where(endPoint => endPoint != null).ToList();
            }
            catch (JsonException) {}

            return null;
        }

        private static IPEndPoint StringToIpEndpoint(string endPoint)
        {
            var epStrings = endPoint.Split(':');

            if (epStrings.Length != 2 || !IPAddress.TryParse(epStrings[0], out IPAddress ipAddress) ||
                !int.TryParse(epStrings[1], NumberStyles.None, NumberFormatInfo.CurrentInfo, out int port))
                return null;

            return new IPEndPoint(ipAddress, port);
        }
    }
}