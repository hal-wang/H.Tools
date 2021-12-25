using Microsoft.Toolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace HTools.Uwp.Controls.Setting
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
            DefaultStyleKey = typeof(SettingSplitView);
            DefaultStyleResourceUri = new System.Uri("ms-appx:///HTools/Themes/uap_generic.xaml");
            CloseCommand = new RelayCommand(() => IsPaneOpen = false);
            RequestedTheme = Window.Current.Content is not FrameworkElement element ? ElementTheme.Light : element.RequestedTheme;
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


        /// <summary>
        /// 
        /// </summary>
        public Thickness ContentPadding
        {
            get { return (Thickness)GetValue(ContentPaddingProperty); }
            set { SetValue(ContentPaddingProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ContentPaddingProperty =
            DependencyProperty.Register("ContentPadding", typeof(Thickness), typeof(SettingSplitView), new PropertyMetadata(new Thickness(12, 20, 15, 0)));


        /// <summary>
        /// 
        /// </summary>
        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(SettingSplitView), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));


        /// <summary>
        /// 
        /// </summary>
        public Brush HeaderForeground
        {
            get { return (Brush)GetValue(HeaderForegroundProperty); }
            set { SetValue(HeaderForegroundProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty HeaderForegroundProperty =
            DependencyProperty.Register("HeaderForeground", typeof(Brush), typeof(SettingSplitView), new PropertyMetadata(ResourcesHelper.GetResource<SolidColorBrush>("ApplicationForegroundThemeBrush")));


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


        public double PaneWidth
        {
            get { return (double)GetValue(PaneWidthProperty); }
            set { SetValue(PaneWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PaneWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PaneWidthProperty =
            DependencyProperty.Register("PaneWidth", typeof(double), typeof(SettingSplitView), new PropertyMetadata(340));


        public SplitViewPanePlacement PanePlacement
        {
            get { return (SplitViewPanePlacement)GetValue(PanePlacementProperty); }
            set { SetValue(PanePlacementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PanePlacement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PanePlacementProperty =
            DependencyProperty.Register("PanePlacement", typeof(SplitViewPanePlacement), typeof(SettingSplitView), new PropertyMetadata(SplitViewPanePlacement.Right));


        /// <summary>
        /// 
        /// </summary>
        public async Task ShowAsync()
        {
            IsOpen = true;
            await TaskExtend.SleepAsync(100);
            IsPaneOpen = true;

            var showWaiter = new TaskCompletionSource<object>();

            void OnIsOpenChanged(bool isOpen)
            {
                if (isOpen) return;

                showWaiter.SetResult(null);
                IsOpenChanged -= OnIsOpenChanged;
            }

            IsOpenChanged += OnIsOpenChanged;
            await showWaiter.Task;
        }

    }
}
