using System.Windows;

namespace H.Tools.Wpf;

public class WindowCloseHandler
{
    private Window _window = null!;
    public async Task<bool> Init(Window window)
    {
        if (_window != null) throw new Exception();
        _window = window;

        if (!await OnBeforeOpening())
        {
            return false;
        }

        window.Closing += MainWindow_Closing;
        window.Loaded += MainWindow_Loaded;
        return true;
    }

    #region Closing
    private bool _closed = false;
    private async void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        if (e.Cancel) return;
        if (_closed) return;

        e.Cancel = true;

        var beforeFuncs = BeforeClosing?.GetInvocationList()?.Reverse() ?? [];
        foreach (var func in beforeFuncs.Cast<Func<Task<bool>>>())
        {
            if (!await func())
            {
                return;
            }
        }

        var afterFuncs = AfterClosing?.GetInvocationList()?.Reverse() ?? [];
        foreach (var func in afterFuncs.Cast<Func<Task>>())
        {
            await func();
        }

        _closed = true;
        _window.Close();
    }

    public event Func<Task<bool>>? BeforeClosing;
    public event Func<Task>? AfterClosing;
    #endregion

    #region Opening
    private async Task<bool> OnBeforeOpening()
    {
        var beforeFuncs = BeforeOpening?.GetInvocationList()?.Reverse() ?? [];
        foreach (var func in beforeFuncs.Cast<Func<Task<bool>>>())
        {
            if (!await func())
            {
                return false;
            }
        }
        return true;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        var afterFuncs = AfterOpening?.GetInvocationList()?.Reverse() ?? [];
        foreach (var func in afterFuncs.Cast<Func<Task>>())
        {
            await func();
        }
    }

    public event Func<Task>? AfterOpening;
    public event Func<Task<bool>>? BeforeOpening;
    #endregion
}
