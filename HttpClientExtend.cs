using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HTools
{
    public static class HttpClientExtend
    {
        public async static Task<HttpResponseMessage> SendAsync(this HttpClient httpClient, string requestUri, string method, object content = null, object param = null, object query = null)
        {
            if (query != null)
            {
                var properties = query.GetType().GetProperties();
                foreach (var property in properties)
                {
                    requestUri = requestUri.Replace($":{property.Name}", property.GetValue(query)?.ToString());
                }
            }
            if (param != null)
            {
                var properties = param.GetType().GetProperties();
                var paramStr = new StringBuilder();
                foreach (var property in properties)
                {
                    paramStr.Append(property.Name);
                    paramStr.Append("=");
                    paramStr.Append(property.GetValue(param));
                    paramStr.Append("&");
                }
                if (paramStr.Length > 0) paramStr.Remove(paramStr.Length - 1, 1);
                requestUri += $"?{paramStr}";
            }

            if (content == null) content = new { };

            var stringContent = new StringContent(JsonConvert.SerializeObject(content));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var request = new HttpRequestMessage(new HttpMethod(method), requestUri)
            {
                Content = stringContent
            };

            Debug.WriteLine("send-start: " + requestUri);
            var res = await httpClient.SendAsync(request);
#if DEBUG
            Debug.WriteLine("send-end: " + requestUri);
            Debug.WriteLine("send-result: " + await res.Content.ReadAsStringAsync());
#endif
            return res;
        }

        public static Task<HttpResponseMessage> GetAsync(this HttpClient httpClient, string requestUri, object content = null, object param = null, object query = null)
        {
            return httpClient.SendAsync(requestUri, "GET", content, param, query);
        }

        public static Task<HttpResponseMessage> PostAsync(this HttpClient httpClient, string requestUri, object content = null, object param = null, object query = null)
        {
            return httpClient.SendAsync(requestUri, "POST", content, param, query);
        }

        public static Task<HttpResponseMessage> PatchAsync(this HttpClient httpClient, string requestUri, object content = null, object param = null, object query = null)
        {
            return httpClient.SendAsync(requestUri, "PATCH", content, param, query);
        }

        public static Task<HttpResponseMessage> DeleteAsync(this HttpClient httpClient, string requestUri, object content = null, object param = null, object query = null)
        {
            return httpClient.SendAsync(requestUri, "DELETE", content, param, query);
        }

        public static Task<HttpResponseMessage> PutAsync(this HttpClient httpClient, string requestUri, object content = null, object param = null, object query = null)
        {
            return httpClient.SendAsync(requestUri, "PUT", content, param, query);
        }
    }
}
