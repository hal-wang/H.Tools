using HTools.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UwpTest.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LayoutDialogPage : Page
    {
        public LayoutDialogPage()
        {
            this.InitializeComponent();
        }

        private async void ShowDialog_Click(object sender, RoutedEventArgs e)
        {
            await PopupHelper.ShowDialog(contentTB.Text, titleTB.Text, primaryButtonTextTB.Text, secondButtonTextTB.Text, isPrimaryDefaultCB.IsChecked, isExitButtonVisibleTS.IsOn, closeButtonTextTB.Text);
        }

        private async void ShowTip_Click(object sender, RoutedEventArgs e)
        {
            await PopupHelper.ShowDialog("test", isExitButtonVisible: true);
        }
    }
}
