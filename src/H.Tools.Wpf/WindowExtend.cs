using H.Tools.Config;
using System.Windows;

namespace H.Tools.Wpf;

public static class WindowExtend
{
    public static T StoreWindowState<T>(this T window, AppSettingsConfiguration? configuration = null) where T : Window
    {
        _ = new WindowStateHandler(window, configuration);
        return window;
    }
}
