using Hubery.Tools.Uwp.Helpers;
using Microsoft.Toolkit.Uwp.UI.Helpers;
using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

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

            new ThemeListener().ThemeChanged += OnThemeChanged;
        }

        private void OnThemeChanged(ThemeListener sender)
        {
            Application.Current.Resources["ThemeForeground"] = Helpers.ColorHelper.IsDarkColor(ResourcesHelper.GetResource<Color>("SystemAccentColor")) ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Black);
        }
    }
}
