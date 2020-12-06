using Hubery.Tools.Uwp.Helpers;
using System;
using System.Collections.Generic;
using Windows.UI;
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
            "ms-appx:///Hubery.Tools/Uwp/Themes/Generic.xaml",
        };

        /// <summary>
        /// 
        /// </summary>
        public HbrUwpResources()
        {
            AddResources();
            StartTiemeListener();
        }

        private void AddResources()
        {
            var res = new ResourceDictionary();
            res.Add(KeyValuePair.Create<object, object>("ThemeForegroundColor", ThemeHelper.ThemeForegroundColor));
            MergedDictionaries.Add(res);

            foreach (var path in _paths)
            {
                MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(path, UriKind.RelativeOrAbsolute) });
            }
        }

        private Color? _beforeThemeColor = null;
        private void StartTiemeListener()
        {
            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += (ss, ee) =>
            {
                var themeColor = ResourcesHelper.GetResource<Color>("SystemAccentColor");
                if (_beforeThemeColor == null || _beforeThemeColor.Value != themeColor)
                {
                    _beforeThemeColor = themeColor;
                    Application.Current.Resources["ThemeForegroundColor"] = ThemeHelper.ThemeForegroundColor;
                }
            };
            timer.Start();
        }
    }
}
