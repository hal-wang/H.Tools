﻿using System.Windows;

namespace H.Tools.Wpf;

public static class WindowExtend
{
    public static T StoreWindowState<T>(this T window) where T : Window
    {
        _ = new WindowStateHandler(window);
        return window;
    }
}
