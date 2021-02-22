using Microsoft.Toolkit.Uwp;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace HTools.Uwp.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class EmojiSelecter : UserControl
    {
        internal IncrementalLoadingCollection<EmojiSelecterSource, string> Emojis { get; } = new IncrementalLoadingCollection<EmojiSelecterSource, string>();

        /// <summary>
        /// 
        /// </summary>
        public EmojiSelecter()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public event TypedEventHandler<EmojiSelecter, EmojiSelecterChangedEventArgs> Changed;

        private string _old = null;
        private void Emoji_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as string;
            Changed?.Invoke(this, new EmojiSelecterChangedEventArgs()
            {
                New = item,
                Old = _old
            });
            _old = item;
        }
    }
}
