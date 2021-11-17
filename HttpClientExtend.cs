using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            if (param != null)
            {
                var properties = param.GetType().GetProperties();
                foreach (var property in properties)
                {
                    requestUri = requestUri.Replace($":{property.Name}", property.GetValue(param)?.ToString());
                }
            }
            if (query != null)
            {
                var properties = query.GetType().GetProperties();
                var paramStr = new StringBuilder();
                foreach (var property in properties)
                {
                    paramStr.Append(property.Name);
                    paramStr.Append("=");
                    paramStr.Append(property.GetValue(query));
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

        public async static Task<T> GetContent<T>(this HttpResponseMessage httpResponse)
        {
            var str = await httpResponse.Content.ReadAsStringAsync();
            if (typeof(T) == typeof(string))
            {
                if (str.Length > 0 && str[0] != '"')
                {
                    str = '"' + str + '"';
                }
            }

            return JsonConvert.DeserializeObject<T>(str);
        }

        public async static Task<string> GetErrorMessage(this HttpResponseMessage httpResponse)
        {
            if (httpResponse.IsSuccessStatusCode) return null;

            string errMsg;
            var contentStr = await httpResponse.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(contentStr))
            {
                errMsg = (int)(httpResponse.StatusCode) + "  " + httpResponse.ReasonPhrase;
            }
            else
            {
                try
                {
                    var jObj = JsonConvert.DeserializeObject<JObject>(contentStr);
                    if (jObj.ContainsKey("message"))
                    {
                        errMsg = jObj.GetValue("message").Value<string>();
                    }
                    else
                    {
                        errMsg = jObj.Value<string>();
                    }
                }
                catch (JsonReaderException)
                {
                    errMsg = contentStr;
                }
            }
            if (errMsg == null) errMsg = "";
            Debug.WriteLine("Error：" + errMsg);
            return errMsg;
        }
    }
}
