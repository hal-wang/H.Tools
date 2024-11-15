using H.Tools.Config;
using System.Windows;

namespace H.Tools.Wpf;

internal class WindowStateHandler
{
    private readonly AppSettingsConfiguration _configuration;
    private readonly Window _window;
    public WindowStateHandler(Window window, AppSettingsConfiguration? configuration)
    {
        _window = window;
        _configuration = configuration ?? new();

        if (window.IsLoaded)
        {
            Window_Loaded(window, new RoutedEventArgs());
        }
        else
        {
            window.Loaded += Window_Loaded;
        }
    }

    private string WindowName => string.IsNullOrEmpty(_window.Name) ? _window.GetType().ToString() : _window.Name;

    private void RestoreSizeConfig()
    {
        var width = _configuration.Get<double>(0, $"{WindowName}_Width");
        var height = _configuration.Get<double>(0, $"{WindowName}_Height");
        var left = _configuration.Get<double>(0, $"{WindowName}_Left");
        var top = _configuration.Get<double>(0, $"{WindowName}_Top");

        if (width == 0 || height == 0) return;

        width = Math.Max(width, 140);
        width = Math.Min(width, SystemParameters.VirtualScreenWidth);
        height = Math.Max(height, 100);
        height = Math.Min(height, SystemParameters.VirtualScreenHeight);

        left = Math.Max(left, 0);
        left = Math.Min(left, SystemParameters.VirtualScreenWidth - width);
        top = Math.Max(top, 0);
        top = Math.Min(top, SystemParameters.VirtualScreenHeight - height);

        _window.Left = left;
        _window.Top = top;
        _window.Width = width;
        _window.Height = height;
    }

    private void RestoreIsMaxSizeConfig()
    {
        var isMaxSize = _configuration.Get(false, $"{WindowName}_IsMaxSize");
        _window.WindowState = isMaxSize ? WindowState.Maximized : WindowState.Normal;
    }

    private Timer? _saveSizeTimer;
    private void SaveSizeConfig(bool force = false)
    {
        if (_window.WindowState != WindowState.Normal) return;

        void Save()
        {
            if (_window.Width <= 0 || _window.Height <= 0) return;

            _configuration.Set(_window.Width, $"{WindowName}_Width");
            _configuration.Set(_window.Height, $"{WindowName}_Height");
            _configuration.Set(_window.Left, $"{WindowName}_Left");
            _configuration.Set(_window.Top, $"{WindowName}_Top");
        }

        _saveSizeTimer?.Dispose();
        _saveSizeTimer = null;
        if (force)
        {
            Save();
        }
        else
        {
            _saveSizeTimer = new Timer((_) =>
            {
                Application.Current.Dispatcher.BeginInvoke(() => Save());
            }, null, 500, Timeout.Infinite);
        }
    }

    private void SaveIsMaxSizeConfig()
    {
        _configuration.Set(_window.WindowState == WindowState.Maximized, $"{WindowName}_IsMaxSize");
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        RestoreSizeConfig();
        RestoreIsMaxSizeConfig();

        _window.Closing += Window_Closing;
        _window.StateChanged += Window_StateChanged;
        _window.SizeChanged += Window_SizeChanged;
        _window.LocationChanged += Window_LocationChanged;
    }

    private void Window_LocationChanged(object? sender, EventArgs e)
    {
        SaveSizeConfig();
    }

    private void Window_SizeChanged(object? sender, SizeChangedEventArgs e)
    {
        SaveSizeConfig();
    }

    private void Window_StateChanged(object? sender, EventArgs e)
    {
        if (_window.WindowState == WindowState.Maximized)
        {
            SaveSizeConfig();
        }
        else
        {
            RestoreSizeConfig();
        }
        SaveIsMaxSizeConfig();
    }

    private void Window_Closing(object? sender, EventArgs e)
    {
        SaveSizeConfig(true);
        SaveIsMaxSizeConfig();
    }
}
