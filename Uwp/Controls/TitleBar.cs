using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HTools.Uwp.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class TitleBar : ContentControl
    {
        /// <summary>
        /// 
        /// </summary>
        public TitleBar()
        {
            this.DefaultStyleKey = typeof(TitleBar);
            DefaultStyleResourceUri = new System.Uri("ms-appx:///HTools/Themes/uap_generic.xaml");
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
