using UwpTest.Helpers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UwpTest.Views {
    public sealed partial class SettingConfigPage : Page {
        public SettingConfigPage() {
            InitializeComponent();
        }

        private void GetButton_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(KeyTextBox.Text)) return;

            ValueTextBox.Text = SettingHelper.Instance.Find<string>(KeyTextBox.Text) ?? "";
        }

        private void SetButton_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(KeyTextBox.Text)) return;

            SettingHelper.Instance.Set(ValueTextBox.Text, KeyTextBox.Text);
        }
    }
}
