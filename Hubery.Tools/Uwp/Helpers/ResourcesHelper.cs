using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;

namespace Hubery.Tools.Uwp.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class ResourcesHelper
    {
        private static ResourceLoader ResourceLoader { get; set; } = ResourceLoader.GetForCurrentView();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resource"></param>
        /// <returns></returns>
        public static T GetResource<T>(string resource)
        {
            return (T)Application.Current.Resources[resource];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public static string GetResStr(string resource)
        {
            return ResourceLoader.GetString(resource);
        }
    }
}
