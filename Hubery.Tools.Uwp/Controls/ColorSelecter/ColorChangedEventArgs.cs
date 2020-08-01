using Windows.UI;

namespace Hubery.Common.Uwp.Controls.ColorSelecter
{
    public class ColorChangedEventArgs
    {
        public Color? Old { get; set; }
        public Color? New { get; set; }
    }
}
