using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            "ms-appx:///Hubery.Tools.Uwp/Themes/Generic.xaml",
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
