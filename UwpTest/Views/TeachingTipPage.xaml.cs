using HTools.Uwp.Helpers;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UwpTest.Views {
    public sealed partial class TeachingTipPage : Page {
        public TeachingTipPage() {
            this.InitializeComponent();
        }

        private async void LayoutTeachingTipButton_Click(object sender, RoutedEventArgs e) {
            _ = await PopupHelper.ShowTeachingTipAsync(sender as FrameworkElement, new TeachingTip() {
                Title = "Title",
                Subtitle = "Subtitle",
                Content = "Content",
                IconSource = new Microsoft.UI.Xaml.Controls.SymbolIconSource() {
                    Symbol = Symbol.Accept
                },
            });
        }

        private async void SimpleTeachingTipButton_Click(object sender, RoutedEventArgs e) {
            await PopupHelper.ShowTeachingTipAsync((sender as FrameworkElement), "Title", "Content");
        }
    }
}
