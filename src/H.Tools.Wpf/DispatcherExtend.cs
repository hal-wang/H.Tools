using System.Windows.Threading;

namespace H.Tools.Wpf;

public static class DispatcherExtend
{
    public static void TryInvoke(this Dispatcher dispatcher, Action callback)
    {
        if (dispatcher.CheckAccess())
        {
            callback();
        }
        else
        {
            dispatcher.Invoke(callback);
        }
    }

    public static void TryInvoke(this Dispatcher dispatcher, Action callback, DispatcherPriority priority)
    {
        if (dispatcher.CheckAccess())
        {
            callback();
        }
        else
        {
            dispatcher.Invoke(callback, priority);
        }
    }

    public static void TryInvoke(this Dispatcher dispatcher, Action callback, DispatcherPriority priority, CancellationToken cancellationToken)
    {
        if (dispatcher.CheckAccess())
        {
            callback();
        }
        else
        {
            dispatcher.Invoke(callback, priority, cancellationToken);
        }
    }

    public static void TryInvoke(this Dispatcher dispatcher, Action callback, DispatcherPriority priority, TimeSpan timeout, CancellationToken cancellationToken)
    {
        if (dispatcher.CheckAccess())
        {
            callback();
        }
        else
        {
            dispatcher.Invoke(callback, priority, cancellationToken, timeout);
        }
    }

    public static async Task TryInvokeAsync(this Dispatcher dispatcher, Action callback)
    {
        if (dispatcher.CheckAccess())
        {
            callback();
        }
        else
        {
            await dispatcher.InvokeAsync(callback);
        }
    }

    public static async Task TryInvokeAsync(this Dispatcher dispatcher, Action callback, DispatcherPriority priority)
    {
        if (dispatcher.CheckAccess())
        {
            callback();
        }
        else
        {
            await dispatcher.InvokeAsync(callback, priority);
        }
    }

    public static async Task TryInvokeAsync(this Dispatcher dispatcher, Action callback, DispatcherPriority priority, CancellationToken cancellationToken)
    {
        if (dispatcher.CheckAccess())
        {
            callback();
        }
        else
        {
            await dispatcher.InvokeAsync(callback, priority, cancellationToken);
        }
    }

    public static async Task<TResult> TryInvokeAsync<TResult>(this Dispatcher dispatcher, Func<TResult> callback)
    {
        if (dispatcher.CheckAccess())
        {
            return callback();
        }
        else
        {
            return await dispatcher.InvokeAsync(callback);
        }
    }

    public static async Task<TResult> TryInvokeAsync<TResult>(this Dispatcher dispatcher, Func<TResult> callback, DispatcherPriority priority)
    {
        if (dispatcher.CheckAccess())
        {
            return callback();
        }
        else
        {
            return await dispatcher.InvokeAsync(callback, priority);
        }
    }

    public static async Task<TResult> TryInvokeAsync<TResult>(this Dispatcher dispatcher, Func<TResult> callback, DispatcherPriority priority, CancellationToken cancellationToken)
    {
        if (dispatcher.CheckAccess())
        {
            return callback();
        }
        else
        {
            return await dispatcher.InvokeAsync(callback, priority, cancellationToken);
        }
    }

    public static void TryBeginInvoke(this Dispatcher dispatcher, Action callback)
    {
        if (dispatcher.CheckAccess())
        {
            callback();
        }
        else
        {
            dispatcher.BeginInvoke(callback);
        }
    }

    public static void TryBeginInvoke(this Dispatcher dispatcher, Action callback, DispatcherPriority priority)
    {
        if (dispatcher.CheckAccess())
        {
            callback();
        }
        else
        {
            dispatcher.BeginInvoke(callback, priority);
        }
    }
}
