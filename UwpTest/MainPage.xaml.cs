using HTools;
using HTools.Uwp.Helpers;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UwpTest
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            await TaskExtend.SleepAsync(1000);
            await PopupHelper.ShowDialog("Test", secondButtonText: "Cancel");


            var time = new DateTime(1970, 1, 1, 0, 0, 0) + TimeSpan.FromSeconds(1609838321);
            var localTime = time.ToLocalTime();

            var time2 = DateTimeOffset.FromUnixTimeSeconds(1609838321);
            var localTime2 = time2.LocalDateTime;

            var b = localTime == localTime2;
            var b2 = time == localTime2;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new TestDialog();
            await dialog.ShowAsync();

            await PopupHelper.ShowTeachingTip(sender as FrameworkElement, "TeachingTip", "TeachingTipContent");
        }
    }
}
