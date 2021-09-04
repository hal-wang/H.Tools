using HTools;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UwpTest.Views {
    public sealed partial class SettingPage : Page {
        public SettingPage() {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e) {
            SettingSplitView.Visibility = Visibility.Visible;
            await TaskExtend.SleepAsync(200);
            await SettingSplitView.ShowAsync();
            await TaskExtend.SleepAsync(200);
            SettingSplitView.Visibility = Visibility.Collapsed;
        }
    }
}
