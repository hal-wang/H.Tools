using Windows.UI.Xaml;

namespace Hubery.Tools.Uwp.Controls.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BoolSettingCell : BaseSettingCell
    {
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
        internal static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(bool), typeof(BaseSettingCell), new PropertyMetadata(false));
    }
}
