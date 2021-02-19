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
    public class HToolsResources : ResourceDictionary
    {
        private readonly string _genericPath = "Themes/Generic.xaml";

        /// <summary>
        /// 
        /// </summary>
        public HToolsResources()
        {
            AddResources();
            StartThemeListener();
        }

        private void AddResources()
        {
            var res = new ResourceDictionary();

#if UAP10_0_18362
            res.Add(KeyValuePair.Create<object, object>("ThemeForegroundColor", ThemeHelper.ThemeForegroundColor));
            MergedDictionaries.Add(res);
#endif

            var genericDict = new ResourceDictionary() { Source = new Uri(FillUrl(_genericPath), UriKind.Absolute) };
            MergedDictionaries.Add(genericDict);
        }

        private Color? _beforeThemeColor = null;
        private void StartThemeListener()
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

        private string FillUrl(string url)
        {
#if UAP10_0_18362
            return $"ms-appx:///HTools/{url}";
#endif
#if NET452
            return $"pack://application:,,,/HTools;component/{url}";
#endif
        }
    }
#endif
}
