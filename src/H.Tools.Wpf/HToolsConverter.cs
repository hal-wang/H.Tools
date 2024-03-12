using System.Windows;

namespace H.Tools.Wpf;

public class HToolsConverter : ResourceDictionary
{
    public HToolsConverter()
    {
        var url = $"pack://application:,,,/H.Tools.Wpf;component/Converters.xaml";
        MergedDictionaries.Add(new() { Source = new Uri(url, UriKind.Absolute) });
    }
}
