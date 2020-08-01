using Windows.UI.Xaml.Media.Animation;

namespace Hubery.Common.Uwp.Models
{
    public class NavParam
    {
        public NavParam(string pageName, object param = null, NavigationTransitionInfo navigationTransitionInfo = null)
        {
            PageName = pageName;
            Param = param;
            NavigationTransitionInfo = navigationTransitionInfo;
        }

        public string PageName { get; set; }
        public object Param { get; set; }
        public NavigationTransitionInfo NavigationTransitionInfo { get; set; }
    }
}