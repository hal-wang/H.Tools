#if UAP10_0_18362
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
#endif

#if NET452
using System.Windows;
using System.Windows.Media;
#endif

namespace HTools
{
#if UAP10_0_18362 || NET452

    /// <summary>
    /// 
    /// </summary>
    public class ResourcesHelper
    {
#if UAP10_0_18362
        private static ResourceLoader ResourceLoader { get; set; } = ResourceLoader.GetForCurrentView();
#endif

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


#if UAP10_0_18362
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public static string GetResStr(string resource)
        {
            return ResourceLoader.GetString(resource);
        }
#endif
    }
#endif
}
