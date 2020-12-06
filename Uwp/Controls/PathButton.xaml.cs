using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Hubery.Tools.Uwp.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class PathButton : Button
    {
        /// <summary>
        /// 
        /// </summary>
        public PathButton()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public Geometry Data
        {
            get { return (Geometry)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(Geometry), typeof(PathButton), new PropertyMetadata(null));
    }
}
