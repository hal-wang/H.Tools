using Hubery.Tools.Uwp.Helpers;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;


namespace Hubery.Tools.Uwp
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApiExtend
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <returns></returns>
        public async static Task<HttpResponseMessage> IsResErr(this HttpResponseMessage httpResponse)
        {
            if (!httpResponse.IsSuccessStatusCode)
            {
                var content = await httpResponse.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content))
                {
                    try
                    {
                        content = (int)(httpResponse.StatusCode) + "  " + httpResponse.StatusCode.ToString();
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Instance.Log(ex);
                        content = "Unknow error";
                    }
                }
                Debug.WriteLine("Error：" + content);
                MessageHelper.ShowDanger(content);
            }
            return httpResponse;
        }
    }
}
