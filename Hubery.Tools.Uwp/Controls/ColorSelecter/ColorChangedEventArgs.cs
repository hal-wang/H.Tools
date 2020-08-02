using Windows.UI;

namespace Hubery.Tools.Uwp.Controls.ColorSelecter
{
    public class ColorChangedEventArgs
    {
        public Color? Old { get; set; }
        public Color? New { get; set; }
    }
}
