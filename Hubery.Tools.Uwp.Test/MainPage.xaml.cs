using Hubery.Tools.Uwp.Helpers;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Hubery.Tools.Uwp.Test
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources.Add("SystemAccentBrush", new SolidColorBrush(Colors.Red));
            //var brush = ResourcesHelper.GetResource<SolidColorBrush>("SystemAccentBrush");
            MessageHelper.ShowDanger("ShowDanger", 0);
            MessageHelper.ShowPrimary("ShowPrimary", 0);
            MessageHelper.ShowInfo("ShowInfo", 0);
            MessageHelper.ShowWarning("ShowWarning", 0);

            //LoadingHelper.Show();
        }
    }
}
