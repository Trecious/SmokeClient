using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;

namespace SomkeClient
{
    class Web
    {
        public string Fetch(string url, string method = "get", Dictionary<string, string> data = null,
            string referer = "", CookieContainer cookies = null)
        {
            var isGetMethod = method.ToLower() == "get";

            if (isGetMethod)
                url += "?" + DataToUrlString(data);

            var webRequest = (HttpWebRequest) WebRequest.Create(url);

            webRequest.Method = method;
            webRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            webRequest.Accept = "application/json, text/javascript;q=0.9, */*;q=0.5";
            webRequest.UserAgent =
                "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.66 Safari/537.36";
            webRequest.Referer = referer;
            webRequest.Timeout = 60000;
            webRequest.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Revalidate);
            webRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            webRequest.CookieContainer = cookies ?? new CookieContainer();

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
