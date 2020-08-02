using System;
using Windows.UI.Xaml;

namespace Hubery.Tools.Uwp
{
    /// <summary>
    /// 
    /// </summary>
    public class HbrUwpResources : ResourceDictionary
    {
        private readonly string[] _paths =
        {
            "ms-appx:///Hubery.Tools.Uwp/Converters/ConvertersDict.xaml",
            "ms-appx:///Hubery.Tools.Uwp/Styles/Resources.xaml",
        };
        
        /// <summary>
        /// 
        /// </summary>
        public HbrUwpResources()
        {
            foreach (var path in _paths)
            {
                MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(path, UriKind.RelativeOrAbsolute) });
            }
        }
    }
}
