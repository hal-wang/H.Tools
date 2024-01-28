using System.Windows;

namespace H.Tools.Converter;

public class HToolsConverter : ResourceDictionary
{
    public HToolsConverter()
    {
        var url = $"pack://application:,,,/H.Tools.Converters;component/ConverterDictionary.xaml";
        MergedDictionaries.Add(new() { Source = new Uri(url, UriKind.Absolute) });
    }
}
