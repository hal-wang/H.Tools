using System.Windows;

namespace H.Tools.Converter;

public class HToolsConverter : ResourceDictionary
{
    public HToolsConverter()
    {
        var url = $"pack://application:,,,/H.Tools.Converter;component/Converters.xaml";
        MergedDictionaries.Add(new() { Source = new Uri(url, UriKind.Absolute) });
    }
}
