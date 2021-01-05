using System.Collections.ObjectModel;
using System.Linq;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HTools.Uwp.Controls.Color
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class ColorSelecter : UserControl
    {
        private static readonly Windows.UI.Color[] _colors = new Windows.UI.Color[]
        {
            Colors.Black,
            Colors.Gray,
            Colors.White,
            Colors.Red,
            Colors.DarkRed,
            Colors.OrangeRed,
            Colors.Orange,
            Colors.LightYellow,
            Colors.Yellow,
            Colors.LightGreen,
            Colors.Green,
            Colors.DarkGreen,
            Colors.LightCyan,
            Colors.Cyan,
            Colors.LightBlue,
            Colors.DeepSkyBlue,
            Colors.Blue,
            Colors.Purple
        };

        /// <summary>
        /// 
        /// </summary>
        public ColorSelecter()
        {
            this.InitializeComponent();
        }


        #region DependencyProperty
        #region Default
        /// <summary>
        /// 
        /// </summary>
        public Windows.UI.Color Default
        {
            get { return (Windows.UI.Color)GetValue(DefaultProperty); }
            set { SetValue(DefaultProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty DefaultProperty =
            DependencyProperty.Register("Default", typeof(Windows.UI.Color), typeof(ColorSelecter), new PropertyMetadata((Windows.UI.Color)Application.Current.Resources["SystemAccentColor"], DefaultChanged));

        private static void DefaultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var selecter = d as ColorSelecter;
            selecter.SelectedColor = selecter.Default;
        }
        #endregion


        #region SelectedColor
        /// <summary>
        /// 
        /// </summary>
        public Windows.UI.Color SelectedColor
        {
            get { return (Windows.UI.Color)GetValue(SelectedColorProperty); }
            set
            {
                if (SelectedColor == value)
                {
                    return;
                }
                SetValue(SelectedColorProperty, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(Windows.UI.Color), typeof(ColorSelecter), new PropertyMetadata((Windows.UI.Color)Application.Current.Resources["SystemAccentColor"]));
        #endregion


        #region LastColor
        /// <summary>
        /// 上次选择的颜色
        /// </summary>
        internal Windows.UI.Color LastColor
        {
            get { return (Windows.UI.Color)GetValue(LastColorProperty); }
            set { SetValue(LastColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LastColor.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty LastColorProperty =
            DependencyProperty.Register("LastColor", typeof(Windows.UI.Color), typeof(ColorSelecter), new PropertyMetadata((Windows.UI.Color)Application.Current.Resources["SystemAccentColor"]));
        #endregion


        #region IsAlphaEnabled
        /// <summary>
        /// 
        /// </summary>
        public bool IsAlphaEnabled
        {
            get { return (bool)GetValue(IsAlphaEnabledProperty); }
            set { SetValue(IsAlphaEnabledProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IsAlphaEnabledProperty =
            DependencyProperty.Register("IsAlphaEnabled", typeof(bool), typeof(ColorSelecter), new PropertyMetadata(false));
        #endregion


        #region PresetOpacity
        /// <summary>
        /// 预设颜色的透明度
        /// </summary>
        public double PresetOpacity
        {
            get { return (double)GetValue(PresetOpacityProperty); }
            set { SetValue(PresetOpacityProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty PresetOpacityProperty =
            DependencyProperty.Register("PresetOpacity", typeof(double), typeof(ColorSelecter), new PropertyMetadata(1.0, PresetOpacityChanged));

        private static void PresetOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var selecter = d as ColorSelecter;
            if (selecter.PresetOpacity < 1)
            {
                if (!selecter.IsAlphaEnabled)
                {
                    selecter.IsAlphaEnabled = true;
                }

                if (selecter.SelectedColor == selecter.Default)
                {
                    var color = selecter.Default;
                    color.A = (byte)(int)(selecter.PresetOpacity * 255);
                    selecter.SelectedColor = color;
                }
            }
        }
        #endregion


        #region Resetable
        /// <summary>
        /// 
        /// </summary>
        public bool Resetable
        {
            get { return (bool)GetValue(ResetableProperty); }
            set { SetValue(ResetableProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ResetableProperty =
            DependencyProperty.Register("Resetable", typeof(bool), typeof(ColorSelecter), new PropertyMetadata(false));
        #endregion
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<ColorItem> DefaultColors = new ObservableCollection<ColorItem>(_colors.Select(color => new ColorItem(color)).ToList());

        private Windows.UI.Color? _old = null;

        /// <summary>
        /// 
        /// </summary>
        public event TypedEventHandler<ColorSelecter, ColorChangedEventArgs> Changed;


        #region Handle
        /// <summary>
        /// 
        /// </summary>
        public void HandleAccept()
        {
            LastColor = SelectedColor;
            ChangeColor();
        }

        /// <summary>
        /// 
        /// </summary>
        public void HandleLastColorClicked()
        {
            SelectedColor = LastColor;
            ChangeColor();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleColorClicked(object sender, ItemClickEventArgs e)
        {
            var color = (e.ClickedItem as ColorItem).Color;
            if (this.PresetOpacity >= 0 && this.PresetOpacity < 1)
            {
                color.A = (byte)(int)(255 * this.PresetOpacity);
            }

            SelectedColor = color;
            HandleAccept();
        }

        private void ChangeColor()
        {
            Changed?.Invoke(this, new ColorChangedEventArgs()
            {
                New = SelectedColor,
                Old = _old
            });

            _old = SelectedColor;
        }

        /// <summary>
        /// 
        /// </summary>
        public void HandleReset()
        {
            Changed?.Invoke(this, new ColorChangedEventArgs()
            {
                New = null,
                Old = _old
            });

            _old = null;
        }
        #endregion
    }
}
