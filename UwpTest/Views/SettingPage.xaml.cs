using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UwpTest.Views {
    public sealed partial class SettingPage : Page {
        public SettingPage() {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            SettingSplitView.ShowAsync();
        }
    }
}
