using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Hubery.Tools.Uwp.Controls.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public class ExpandSettingCell : BaseSettingCell
    {
        /// <summary>
        /// 
        /// </summary>
        public ExpandSettingCell()
        {
            DefaultStyleKey = typeof(ExpandSettingCell);
        }


        internal bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        internal static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(ExpandSettingCell), new PropertyMetadata(false));

        /// <summary>
        /// 
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var button = (Button)GetTemplateChild("ButtonElement");
            button.Click += (ss, ee) => this.IsOpen = !this.IsOpen;
        }
    }

}
