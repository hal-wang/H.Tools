using Windows.UI.Xaml;

namespace HTools.Uwp.Controls.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SwitchSettingCell : BaseSettingCell
    {
        /// <summary>
        /// 
        /// </summary>
        public SwitchSettingCell()
        {
            DefaultStyleKey = typeof(SwitchSettingCell);
            DefaultStyleResourceUri = new System.Uri("ms-appx:///HTools/Themes/uap_generic.xaml");
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Value
        {
            get { return (bool)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(bool), typeof(BaseSettingCell), new PropertyMetadata(false));
    }
}
