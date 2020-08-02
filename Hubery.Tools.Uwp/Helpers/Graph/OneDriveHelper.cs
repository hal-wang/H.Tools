using Microsoft.Graph;
using System;
using System.Diagnostics;
using Hubery.Common.Base;
using System.Threading.Tasks;

namespace Hubery.Tools.Uwp.Helpers.Graph
{
    public class OneDriveHelper
    {
        //private readonly string[] _scopes = new string[] { "Files.ReadWrite.All" };
        //private GraphServiceClient _graphServiceClient = null;
        //private DateTime _tokenTime;

        //public static OneDriveHelper Instance { get; } = new OneDriveHelper();

        //public async Task<GraphServiceClient> GetGraph()
        //{
        //    if (_graphServiceClient != null && _tokenTime.AddMinutes(30) < DateTime.Now)
        //    {
        //        return _graphServiceClient;
        //    }

        //    try
        //    {
        //        var provider = new DeviceCodeAuthProvider(Constant.GraphClientId, _scopes);
        //        var token = await provider.GetAccessToken().TimeoutAfter(new TimeSpan(0, 0, 0, 10));
        //        if (!string.IsNullOrEmpty(token))
        //        {
        //            _tokenTime = DateTime.Now;
        //            _graphServiceClient = new GraphServiceClient(provider);
        //            return _graphServiceClient;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        PopupHelper.ShowToast("连接OneDrive失败");
        //        Debug.WriteLine(ex);
        //    }

        //    return null;
        //}
    }
}