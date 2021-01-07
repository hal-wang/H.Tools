﻿using System;

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

        private readonly string[] _resourcesPaths =
        {
#if UAP10_0_18362
            "Converters/UwpConverters.xaml",
#endif
#if NET452
            "Converters/WpfConverters.xaml",
#endif

#if UAP10_0_18362
            "Uwp/Themes/Colors.xaml",

            "Uwp/Themes/Button.xaml",
            "Uwp/Themes/TextBox.xaml",
            "Uwp/Themes/PasswordBox.xaml",
            "Uwp/Themes/LayoutTeachingTip.xaml",
            "Uwp/Themes/TitleBar.xaml",

            "Uwp/Controls/Message/MessageStyle.xaml",
            "Uwp/Controls/Dialog/DialogStyle.xaml",
            "Uwp/Controls/Setting/SettingStyle.xaml",
#endif
        };

        /// <summary>
        /// 
        /// </summary>
        public HToolsResources()
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

            var genericDict = new ResourceDictionary() { Source = new Uri(FillUrl(_genericPath), UriKind.RelativeOrAbsolute) };
            foreach (var url in _resourcesPaths)
            {
                genericDict.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(FillUrl(url), UriKind.RelativeOrAbsolute) });
            }

            MergedDictionaries.Add(genericDict);
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

        private string FillUrl(string url)
        {
#if UAP10_0_18362
            return $"ms-appx:///HTools/{url}";
#endif
#if NET452
            return $"/HTools;Component/{url}";
#endif
        }
    }
#endif
}
