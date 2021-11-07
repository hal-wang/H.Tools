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
            DefaultStyleResourceUri = new System.Uri("ms-appx:///HTools/Themes/uap_generic.xaml");
        }

        /// <summary>
        /// 
        /// </summary>
        public event TypedEventHandler<SelectSettingCell, object> ItemClick;

        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(object), typeof(SelectSettingCell), new PropertyMetadata(null));


        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(SelectSettingCell), new PropertyMetadata(null));


        public Style ItemContainerStyle
        {
            get { return (Style)GetValue(ItemContainerStyleProperty); }
            set { SetValue(ItemContainerStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemContainerStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemContainerStyleProperty =
            DependencyProperty.Register("ItemContainerStyle", typeof(Style), typeof(SelectSettingCell), new PropertyMetadata(null));


        public StyleSelector ItemContainerStyleSelector
        {
            get { return (StyleSelector)GetValue(ItemContainerStyleSelectorProperty); }
            set { SetValue(ItemContainerStyleSelectorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemContainerStyleSelector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemContainerStyleSelectorProperty =
            DependencyProperty.Register("ItemContainerStyleSelector", typeof(StyleSelector), typeof(SelectSettingCell), new PropertyMetadata(null));


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
                ItemClick?.Invoke(this, ee.ClickedItem);
                IsOpen = false;
            };

            var button = (Button)GetTemplateChild("ButtonElement");
            button.Click += (ss, ee) => this.IsOpen = !this.IsOpen;
        }
    }
}
