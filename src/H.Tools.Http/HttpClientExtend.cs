using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace H.Tools.Http;

public static class HttpClientExtend
{
    public static string GetPathWithQuery(string requestUri, object query = null)
    {
        if (query is IDictionary<string, object> dictionary)
        {
            var paramStr = new StringBuilder();
            foreach (var kvp in dictionary)
            {
                paramStr.Append(kvp.Key);
                paramStr.Append('=');
                paramStr.Append(WebUtility.UrlEncode(kvp.Value.ToString()));
                paramStr.Append('&');
            }
            requestUri += $"{(requestUri.Contains("?") ? '&' : '?')}{paramStr}";
        }
        else if (query != null)
        {
            var properties = query.GetType().GetProperties();
            var paramStr = new StringBuilder();
            foreach (var property in properties)
            {
                paramStr.Append(property.Name);
                paramStr.Append('=');
                paramStr.Append(WebUtility.UrlEncode(property.GetValue(query)?.ToString()));
                paramStr.Append('&');
            }
            requestUri += $"{(requestUri.Contains("?") ? '&' : '?')}{paramStr}";
        }
        return requestUri;
    }

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
        requestUri = GetPathWithQuery(requestUri, query);

        HttpRequestMessage request;
        if (method.ToUpper() != "GET")
        {
            content ??= new { };

            if (content is not HttpContent httpContent)
            {
                httpContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(content));
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            }
            request = new HttpRequestMessage(new HttpMethod(method), requestUri)
            {
                Content = httpContent
            };
        }
        else
        {
            request = new HttpRequestMessage(new HttpMethod(method), requestUri);
        }


        Debug.WriteLine("send-start: " + requestUri);
        var res = await httpClient.SendAsync(request);
#if DEBUG
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

    public static HttpClient SetTimeout(this HttpClient httpClient, TimeSpan timeSpan)
    {
        httpClient.Timeout = timeSpan;
        return httpClient;
    }

    public static HttpClient SetTimeout(this HttpClient httpClient, int second)
    {
        httpClient.Timeout = TimeSpan.FromSeconds(second);
        return httpClient;
    }
}