using Microsoft.Toolkit.Uwp;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace HTools.Uwp.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class FontSelecter : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public event TypedEventHandler<FontSelecter, FontSelecterChangedEventArgs> Changed;
        internal IncrementalLoadingCollection<FontSelecterSource, string> Fonts { get; } = new IncrementalLoadingCollection<FontSelecterSource, string>();

        /// <summary>
        /// 
        /// </summary>
        public FontSelecter()
        {
            this.InitializeComponent();
        }

        private string _oldFont = null;
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!(e.ClickedItem is string str))
                return;

            Changed?.Invoke(this, new FontSelecterChangedEventArgs()
            {
                Old = _oldFont,
                New = str
            });

            _oldFont = str;
        }
    }
}
