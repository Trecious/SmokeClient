using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SomkeClient
{
    public class GetCmListResponse
    {
        [JsonProperty("response")]
        public GetCmListContent Content { get; set; }
    }

    public class GetCmListContent
    {
        [JsonProperty("serverlist")]
        public string[] Serverlist { get; set; }

        [JsonProperty("result")]
        public EResult Result { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
