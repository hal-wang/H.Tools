using System.Collections.ObjectModel;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Hubery.Tools.Uwp.Controls.ColorSelecter
{
    public sealed partial class ColorSelecter : UserControl
    {
        public ColorSelecter()
        {
            this.InitializeComponent();
        }


        #region DependencyProperty
        #region Default
        public Color Default
        {
            get { return (Color)GetValue(DefaultProperty); }
            set { SetValue(DefaultProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Default.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultProperty =
            DependencyProperty.Register("Default", typeof(Color), typeof(ColorSelecter), new PropertyMetadata((Color)Application.Current.Resources["SystemAccentColor"], DefaultChanged));

        private static void DefaultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var selecter = d as ColorSelecter;
            selecter.SelectedColor = selecter.Default;
        }
        #endregion


        #region SelectedColor
        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set
            {
                if (SelectedColor == value)
                {
                    return;
                }
                SetValue(SelectedColorProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for SelectedColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ColorSelecter), new PropertyMetadata((Color)Application.Current.Resources["SystemAccentColor"]));
        #endregion


        #region LastColor
        /// <summary>
        /// 上次选择的颜色
        /// </summary>
        internal Color LastColor
        {
            get { return (Color)GetValue(LastColorProperty); }
            set { SetValue(LastColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LastColor.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty LastColorProperty =
            DependencyProperty.Register("LastColor", typeof(Color), typeof(ColorSelecter), new PropertyMetadata((Color)Application.Current.Resources["SystemAccentColor"]));
        #endregion


        #region IsAlphaEnabled
        public bool IsAlphaEnabled
        {
            get { return (bool)GetValue(IsAlphaEnabledProperty); }
            set { SetValue(IsAlphaEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAlphaEnabled.  This enables animation, styling, binding, etc...
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

        // Using a DependencyProperty as the backing store for PresetOpacity.  This enables animation, styling, binding, etc...
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
        public bool Resetable
        {
            get { return (bool)GetValue(ResetableProperty); }
            set { SetValue(ResetableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Resetable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResetableProperty =
            DependencyProperty.Register("Resetable", typeof(bool), typeof(ColorSelecter), new PropertyMetadata(false));
        #endregion
        #endregion


        public ObservableCollection<ColorItem> DefaultColors = new ObservableCollection<ColorItem>()
        {
            new ColorItem(Colors.Black, "黑"),
                new ColorItem(Colors.Gray, "灰"),
                new ColorItem(Colors.White, "白"),
                new ColorItem(Colors.Red, "红"),
                new ColorItem(Colors.DarkRed, "深红"),
                new ColorItem(Colors.OrangeRed, "橙红"),
                new ColorItem(Colors.Orange, "橙"),
                new ColorItem(Colors.LightYellow, "浅黄"),
                new ColorItem(Colors.Yellow, "黄"),
                new ColorItem(Colors.LightGreen, "浅绿"),
                new ColorItem(Colors.Green, "绿"),
                new ColorItem(Colors.DarkGreen, "深绿"),
                new ColorItem(Colors.LightCyan, "浅青"),
                new ColorItem(Colors.Cyan, "青"),
                new ColorItem(Colors.LightBlue, "浅蓝"),
                new ColorItem(Colors.DeepSkyBlue, "天蓝"),
                new ColorItem(Colors.Blue, "蓝"),
                new ColorItem(Colors.Purple, "紫"),
        };

        private Color? _old = null;

        public event TypedEventHandler<ColorSelecter, ColorChangedEventArgs> Changed;


        #region Handle
        public void HandleAccept()
        {
            LastColor = SelectedColor;
            ChangeColor();
        }

        public void HandleLastColorClicked()
        {
            SelectedColor = LastColor;
            ChangeColor();
        }

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
