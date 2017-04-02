using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SmokeyLib
{
    class WebAPI
    {
        public static string Call(HttpMethod method, string intface, string function, uint version = 1,
            Dictionary<string, string> data = null, string apiKey = null, bool ssl = false)
        {
            if(data == null)
                data = new Dictionary<string, string>();

            string url = (ssl ? "https://" : "http://") + $"api.steampowered.com/{intface}/{function}/v{version}/";

            data.Add("format", "json");
            
            if(!string.IsNullOrEmpty(apiKey))
                data.Add("key", apiKey);

            return Fetch(url, method, data);
        }

        private static string Fetch(string url, HttpMethod method, Dictionary<string, string> data = null)
        {
            var isGetMethod = method.Equals(HttpMethod.Get);

            if (isGetMethod)
                url += "?" + DataToUrlString(data);

            var webRequest = WebRequest.Create(url);
            
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

        private static string DataToUrlString(Dictionary<string, string> data)
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
