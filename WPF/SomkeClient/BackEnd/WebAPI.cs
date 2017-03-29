using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SomkeClient
{
    class WebAPI
    {
        private string _apiKey;

        public WebAPI(string apiKey)
        {
            _apiKey = apiKey;
        }

        public string Call(HttpMethod method, string intface, string function, int version = 1,
            Dictionary<string, string> data = null, bool ssl = false)
        {
            if(data == null)
                data = new Dictionary<string, string>();

            string url = (ssl ? "https://" : "http://") + $"api.steampowered.com/{intface}/{function}/v{version}";

            data.Add("format", "json");
            
            if(!string.IsNullOrEmpty(_apiKey))
                data.Add("key", _apiKey);

            return Fetch(url, method, data);
        }

        private string Fetch(string url, HttpMethod method, Dictionary<string, string> data = null)
        {
            var isGetMethod = method.Equals(HttpMethod.Get);

            if (isGetMethod)
                url += "?" + DataToUrlString(data);

            var webRequest = (HttpWebRequest) WebRequest.Create(url);
            
            webRequest.Method = method.ToString();
            webRequest.Timeout = 60000;

            if (!isGetMethod && data != null)
            {
                var dataBytes = Encoding.UTF8.GetBytes(DataToUrlString(data));
                webRequest.ContentLength = dataBytes.Length;

                using (var requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(dataBytes, 0, dataBytes.Length);
                }
            }

            using (var responseStream = webRequest.GetResponse().GetResponseStream())
            {
                if (responseStream == null)
                    return "";

                using (var reader = new StreamReader(responseStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private string DataToUrlString(Dictionary<string, string> data)
        {
            var dataString = String.Empty;

            foreach (var pair in data)
            {
                dataString += dataString == String.Empty ? null : "&";
                dataString += $"{WebUtility.UrlDecode(pair.Key)}={WebUtility.UrlDecode(pair.Value)}";
            }

            return dataString;
        }
    }
}
