using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Hubery.Tools.Uwp.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class TitleBar : ContentControl
    {
        /// <summary>
        /// 
        /// </summary>
        public TitleBar()
        {
            this.InitializeComponent();

            Loaded += TitleBar_Loaded;
        }

        private void TitleBar_Loaded(object sender, RoutedEventArgs e)
        {
            Set();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Set()
        {
            Window.Current.SetTitleBar(this);
        }
    }
}
