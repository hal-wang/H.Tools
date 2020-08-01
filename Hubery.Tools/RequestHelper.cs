using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hubery.Tools
{
    public class RequestHelper
    {
        private readonly string _url;
        private readonly string _controller;

        public int Timeout { get; set; } = 20;

        public RequestHelper(string url, string controller = null)
        {
            _url = url;
            _controller = controller;
        }

        private string GetUrl(string func)
        {
            return _url + (_controller == null ? "" : ("/" + _controller)) + "/" + func;
        }

        private HttpClient GetHttpClient(KeyValuePair<string, string>[] header)
        {
            HttpClient client = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(Timeout)
            };
            if (header != null)
            {
                foreach (var kvp in header)
                {
                    client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
                }
            }
            return client;
        }

        private async Task<HttpResponseMessage> Request(string funcName, string method, HttpContent httpContent, KeyValuePair<string, string>[] header)
        {
            using HttpClient client = GetHttpClient(header);
            string url = GetUrl(funcName);

            var response = method switch
            {
                "POST" => await client.PostAsync(url, httpContent),
                "GET" => await client.GetAsync(url),
                _ => throw new NotSupportedException()
            };
            Debug.WriteLine($"{url}, result:{response.StatusCode}, {await response.Content.ReadAsStringAsync()}");

            return response;
        }

        public async Task<HttpResponseMessage> Post(string funcName, HttpContent httpContent, params KeyValuePair<string, string>[] header)
        {
            return await this.Request(funcName, "POST", httpContent, header);
        }

        public async Task<HttpResponseMessage> Post(string funcName, object param = null, params KeyValuePair<string, string>[] header)
        {
            Debug.WriteLine($"{GetUrl(funcName)} POST: {JsonConvert.SerializeObject(param)}");
            return await this.Post(funcName, new StringContent(param == null ? "{}" : JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json"), header);
        }

        public async Task<HttpResponseMessage> Get(string funcName, params KeyValuePair<string, string>[] header)
        {
            return await this.Request(funcName, "GET", null, header);
        }
    }
}
