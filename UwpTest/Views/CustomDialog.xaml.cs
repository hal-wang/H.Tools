using HTools.Uwp.Helpers;
using Windows.UI.Xaml.Controls;

namespace UwpTest.Views
{
    public sealed partial class CustomDialog : ContentDialog, IResultDialog<bool?>
    {
        public CustomDialog()
        {
            this.InitializeComponent();
        }

        public bool? Result { get; private set; } = null;

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            args.Cancel = true;
            this.Hide(true);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Result = false;
        }
    }
}
