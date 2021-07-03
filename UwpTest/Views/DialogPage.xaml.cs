using HTools.Uwp.Helpers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UwpTest.Views
{
    public sealed partial class DialogPage : Page
    {
        public DialogPage()
        {
            this.InitializeComponent();
        }

        private async void ShowDialog_Click(object sender, RoutedEventArgs e)
        {
            await PopupHelper.ShowDialog(contentTB.Text, titleTB.Text, primaryButtonTextTB.Text, secondButtonTextTB.Text, isPrimaryDefaultCB.IsChecked, isExitButtonVisibleTS.IsOn, closeButtonTextTB.Text);
            await PopupHelper.ShowDialog("SecondDialog");
        }

        private async void ShowTip_Click(object sender, RoutedEventArgs e)
        {
            await PopupHelper.ShowDialog("test", isExitButtonVisible: true);
        }
    }
}
