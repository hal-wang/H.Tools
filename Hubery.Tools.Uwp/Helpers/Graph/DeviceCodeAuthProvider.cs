using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Hubery.Tools.Uwp.Helpers.Graph
{
    /// <summary>
    /// 
    /// </summary>
    public class DeviceCodeAuthProvider : IAuthenticationProvider
    {
        private readonly string _appId;
        private readonly string[] _scopes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="scopes"></param>
        public DeviceCodeAuthProvider(string appId, string[] scopes)
        {
            _scopes = scopes;
            _appId = appId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAccessToken()
        {
            try
            {
                var app = PublicClientApplicationBuilder.Create(_appId).Build();
                var accounts = await app.GetAccountsAsync();

                AuthenticationResult result;
                try
                {
                    result = await app.AcquireTokenSilent(_scopes, accounts.FirstOrDefault()).ExecuteAsync();
                }
                catch (MsalUiRequiredException)
                {
                    result = await app.AcquireTokenInteractive(_scopes).ExecuteAsync();
                }
                return result.AccessToken;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public async Task AuthenticateRequestAsync(HttpRequestMessage requestMessage)
        {
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", await GetAccessToken());
        }
    }
}