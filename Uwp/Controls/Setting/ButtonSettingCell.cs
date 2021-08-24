using Windows.UI.Xaml;

namespace HTools.Uwp.Controls.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ButtonSettingCell : BaseSettingCell
    {
        /// <summary>
        /// 
        /// </summary>
        public ButtonSettingCell()
        {
            DefaultStyleKey = typeof(ButtonSettingCell);
            DefaultStyleResourceUri = new System.Uri("ms-appx:///HTools/Themes/uap_generic.xaml");
        }

        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(ButtonSettingCell), new PropertyMetadata(string.Empty));
    }
}
