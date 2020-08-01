using Microsoft.Toolkit.Uwp;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Hubery.Common.Uwp.Controls.FontSelecter
{
    public sealed partial class FontSelecter : UserControl
    {
        public event TypedEventHandler<FontSelecter, FontChangedEventArgs> Changed;
        internal IncrementalLoadingCollection<FontSource, string> Fonts { get; } = new IncrementalLoadingCollection<FontSource, string>();

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
