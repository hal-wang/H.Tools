using HTools;
using HTools.Uwp.Helpers;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UwpTest.Views
{
    public sealed partial class SettingPage : Page
    {
        public SettingPage()
        {
            this.InitializeComponent();
        }

        public IList<string> Items => Enum.GetNames(typeof(Visibility));

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            SettingSplitView.Visibility = Visibility.Visible;
            await TaskExtend.SleepAsync(100);
            await SettingSplitView.ShowAsync();
            await TaskExtend.SleepAsync(100);
            SettingSplitView.Visibility = Visibility.Collapsed;
        }

        private void SelectSettingCell_ItemClick(HTools.Uwp.Controls.Setting.SelectSettingCell sender, object args)
        {
            MessageHelper.ShowToast(args.ToString());
        }
    }
}
