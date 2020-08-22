using Windows.UI.Xaml;

namespace Hubery.Tools.Uwp.Controls.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class TextSettingCell : BaseSettingCell
    {
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
            DependencyProperty.Register("Value", typeof(string), typeof(TextSettingCell), new PropertyMetadata(string.Empty));
    }
}
