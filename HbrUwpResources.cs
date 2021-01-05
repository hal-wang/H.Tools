using System;

#if NET452
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
#endif

#if UAP10_0_18362
using HTools.Uwp.Helpers;
using Windows.UI;
using Windows.UI.Xaml;
using System.Collections.Generic;
#endif

namespace HTools
{
#if NET452 || UAP10_0_18362
    /// <summary>
    /// 
    /// </summary>
    public class HbrUwpResources : ResourceDictionary
    {
        private readonly string[] _paths =
        {
            "ms-appx:///HTools/Themes/Generic.xaml",
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

#if UAP10_0_18362
            res.Add(KeyValuePair.Create<object, object>("ThemeForegroundColor", ThemeHelper.ThemeForegroundColor));
            MergedDictionaries.Add(res);
#endif

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

#if UAP10_0_18362
                    Application.Current.Resources["ThemeForegroundColor"] = ThemeHelper.ThemeForegroundColor;
#endif
                }
            };
            timer.Start();
        }
    }
#endif
}
