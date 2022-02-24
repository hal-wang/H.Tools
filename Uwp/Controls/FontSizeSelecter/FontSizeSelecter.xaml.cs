using Microsoft.Toolkit.Uwp;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HTools.Uwp.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class FontSizeSelecter : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public event TypedEventHandler<FontSizeSelecter, FontSizeSelecterChangedEventArgs> Changed;

        /// <summary>
        /// 
        /// </summary>
        public FontSizeSelecter()
        {
            this.InitializeComponent();

            Loaded += FontSizeSelecter_Loaded;
        }

        private void FontSizeSelecter_Loaded(object sender, RoutedEventArgs e)
        {
            FontSizes = new(new FontSizeSelecterSource(MinSize, MaxSize, Step));
        }


        private IncrementalLoadingCollection<FontSizeSelecterSource, double> FontSizes
        {
            get { return (IncrementalLoadingCollection<FontSizeSelecterSource, double>)GetValue(FontSizesProperty); }
            set { SetValue(FontSizesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FontSizes.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty FontSizesProperty =
            DependencyProperty.Register("FontSizes", typeof(IncrementalLoadingCollection<FontSizeSelecterSource, double>), typeof(FontSizeSelecter), new PropertyMetadata(null));


        public double MinSize
        {
            get { return (double)GetValue(MinSizeProperty); }
            set { SetValue(MinSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinSizeProperty =
            DependencyProperty.Register("MinSize", typeof(double), typeof(FontSizeSelecter), new PropertyMetadata(10.0));


        public double MaxSize
        {
            get { return (double)GetValue(MaxSizeProperty); }
            set { SetValue(MaxSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxSizeProperty =
            DependencyProperty.Register("MaxSize", typeof(double), typeof(FontSizeSelecter), new PropertyMetadata(200.0));


        public double Step
        {
            get { return (double)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Step.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register("Step", typeof(double), typeof(FontSizeSelecter), new PropertyMetadata(0.5));


        private double _oldFont;
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is not double num)
            {
                return;
            }

            Changed?.Invoke(this, new FontSizeSelecterChangedEventArgs()
            {
                Old = _oldFont,
                New = num
            });

            _oldFont = num;
        }
    }
}
