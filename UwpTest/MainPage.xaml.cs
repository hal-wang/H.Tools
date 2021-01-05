using HTools;
using HTools.Uwp.Helpers;
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
        }
    }
}
