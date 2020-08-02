using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Hubery.Tools.Uwp.Controls.Loading
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PartialLoading : UserControl, ILoading
    {
        /// <summary>
        /// 
        /// </summary>
        public PartialLoading()
        {
            this.InitializeComponent();
        }

        #region 依赖属性
        /// <summary>
        /// 
        /// </summary>
        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(PartialLoading), new PropertyMetadata(false));

        /// <summary>
        /// 
        /// </summary>
        public bool Clickable
        {
            get { return (bool)GetValue(ClickableProperty); }
            set { SetValue(ClickableProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ClickableProperty =
            DependencyProperty.Register("Clickable", typeof(bool), typeof(ILoading), new PropertyMetadata(false));


        /// <summary>
        /// 
        /// </summary>
        public double BackgroundOpacity
        {
            get { return (double)GetValue(BackgroundOpacityProperty); }
            set { SetValue(BackgroundOpacityProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty BackgroundOpacityProperty =
            DependencyProperty.Register("BackgroundOpacity", typeof(double), typeof(ILoading), new PropertyMetadata(0.0));


        /// <summary>
        /// 
        /// </summary>
        public double PaneOpacity
        {
            get { return (double)GetValue(PaneOpacityProperty); }
            set { SetValue(PaneOpacityProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty PaneOpacityProperty =
            DependencyProperty.Register("PaneOpacity", typeof(double), typeof(ILoading), new PropertyMetadata(0.6));

        /// <summary>
        /// 
        /// </summary>
        public double Size
        {
            get { return (double)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register("Size", typeof(double), typeof(ILoading), new PropertyMetadata(40.0));

        /// <summary>
        /// 
        /// </summary>
        public string LoadingStr
        {
            get { return (string)GetValue(LoadingStrProperty); }
            set { SetValue(LoadingStrProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty LoadingStrProperty =
            DependencyProperty.Register("LoadingStr", typeof(string), typeof(ILoading), new PropertyMetadata(string.Empty));
        #endregion
    }
}
