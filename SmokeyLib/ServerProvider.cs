using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace SmokeyLib
{
    class ServerProvider
    {
        private int _cellId;
        private int _serverNummber;
        private List<IPEndPoint> _cmServers;

        public IPEndPoint GetNextCmServer()
        {
            if (_cmServers == null || _serverNummber == _cmServers.Count)
            {
                _cellId++;
                _serverNummber = 0;
                _cmServers = GetCmServers(_cellId);
            }

            if (_cmServers.Count == 0)
                return null;

            _serverNummber++;
            return _cmServers.ElementAt(_serverNummber);
        }

        private List<IPEndPoint> GetCmServers(int cellId)
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

        private IPEndPoint StringToIpEndpoint(string endPoint)
        {
            var epStrings = endPoint.Split(':');

            if (epStrings.Length != 2 || !IPAddress.TryParse(epStrings[0], out IPAddress ipAddress) ||
                !int.TryParse(epStrings[1], NumberStyles.None, NumberFormatInfo.CurrentInfo, out int port))
                return null;

            return new IPEndPoint(ipAddress, port);
        }
    }
}