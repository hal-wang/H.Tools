using Microsoft.Toolkit.Uwp;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Hubery.Tools.Uwp.Controls.Font
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class FontSelecter : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public event TypedEventHandler<FontSelecter, FontChangedEventArgs> Changed;
        internal IncrementalLoadingCollection<FontSource, string> Fonts { get; } = new IncrementalLoadingCollection<FontSource, string>();

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

            Changed?.Invoke(this, new FontChangedEventArgs()
            {
                Old = _oldFont,
                New = str
            });

            _oldFont = str;
        }
    }
}
