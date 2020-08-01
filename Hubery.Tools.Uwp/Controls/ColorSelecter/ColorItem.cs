using Windows.UI;

namespace Hubery.Common.Uwp.Controls.ColorSelecter
{
    public class ColorItem
    {
        public ColorItem(Color color, string name, bool isDark = false)
        {
            Color = color;
            Name = name;
            IsDark = isDark;
        }

        public string Name { get; set; }
        public Color Color { get; set; }
        public bool IsDark { get; set; }
    }
}