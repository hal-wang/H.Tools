using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HTools.Uwp.Controls.Setting {
    /// <summary>
    /// 
    /// </summary>
    public sealed class SwitchSettingCell : BaseSettingCell {
        /// <summary>
        /// 
        /// </summary>
        public SwitchSettingCell() {
            DefaultStyleKey = typeof(SwitchSettingCell);
            DefaultStyleResourceUri = new System.Uri("ms-appx:///HTools/Themes/uap_generic.xaml");
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Value {
            get => (bool)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(bool), typeof(BaseSettingCell), new PropertyMetadata(false));

        public event RoutedEventHandler Toggled;

        protected override void OnApplyTemplate() {
            base.OnApplyTemplate();

            var ts = (ToggleSwitch)GetTemplateChild("ToggleSwitch");
            ts.Toggled += (s, e) => Toggled?.Invoke(s, e);
        }
    }
}
