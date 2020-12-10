using System.Collections.ObjectModel;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HTools.Uwp.Controls.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public class SelectSettingCell : BaseSettingCell
    {
        /// <summary>
        /// 
        /// </summary>
        public SelectSettingCell()
        {
            DefaultStyleKey = typeof(SelectSettingCell);
        }

        /// <summary>
        /// 
        /// </summary>
        public event TypedEventHandler<SelectSettingCell, object> ItemClick;

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<object> Items
        {
            get { return (ObservableCollection<object>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<object>), typeof(SelectSettingCell), new PropertyMetadata(new ObservableCollection<object>()));


        internal bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        internal static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(SelectSettingCell), new PropertyMetadata(false));


        /// <summary>
        /// 
        /// </summary>
        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(SelectSettingCell), new PropertyMetadata(null));

        /// <summary>
        /// 
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var listView = (ListView)GetTemplateChild("ListViewElement");
            listView.ItemClick += (ss, ee) =>
            {
                ItemClick(this, ee.ClickedItem);
                IsOpen = false;
            };

            var button = (Button)GetTemplateChild("ButtonElement");
            button.Click += (ss, ee) => this.IsOpen = !this.IsOpen;
        }
    }
}
