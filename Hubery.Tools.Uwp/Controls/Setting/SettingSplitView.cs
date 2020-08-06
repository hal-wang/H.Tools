using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace Hubery.Tools.Uwp.Controls.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public class SettingSplitView : PopupLayout
    {
        /// <summary>
        /// 
        /// </summary>
        public SettingSplitView()
        {
            CloseCommand = new RelayCommand(() => IsPaneOpen = false);
            RequestedTheme = !(Window.Current.Content is FrameworkElement element) ? ElementTheme.Light : element.RequestedTheme;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(SettingSplitView), new PropertyMetadata(string.Empty));



        internal ICommand CloseCommand
        {
            get { return (ICommand)GetValue(CloseCommandProperty); }
            set { SetValue(CloseCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseCommand.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty CloseCommandProperty =
            DependencyProperty.Register("CloseCommand", typeof(ICommand), typeof(SettingSplitView), new PropertyMetadata(null));


        /// <summary>
        /// 
        /// </summary>
        public bool IsPaneOpen
        {
            get { return (bool)GetValue(IsPaneOpenProperty); }
            set { SetValue(IsPaneOpenProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IsPaneOpenProperty =
            DependencyProperty.Register("IsPaneOpen", typeof(bool), typeof(SettingSplitView), new PropertyMetadata(false, OnIsPaneOpenChange));

        private async static void OnIsPaneOpenChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue)
            {
                await TaskExtend.SleepAsync(100);
            }
            (d as SettingSplitView).IsOpen = (bool)e.NewValue;
        }

        /// <summary>
        /// 
        /// </summary>
        public async void Show()
        {
            IsOpen = true;
            await TaskExtend.SleepAsync(100);
            IsPaneOpen = true;
        }
    }
}
