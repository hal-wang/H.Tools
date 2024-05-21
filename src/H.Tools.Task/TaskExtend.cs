using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Tools.Task;

public static class TaskExtend
{
    /// <summary>
    /// Task with timeout and return
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="task"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, TimeSpan timeout)
    {
        using var timeoutCancellationTokenSource = new CancellationTokenSource();
        var completedTask = await System.Threading.Tasks.Task.WhenAny(task, System.Threading.Tasks.Task.Delay(timeout, timeoutCancellationTokenSource.Token));
        if (completedTask == task)
        {
            timeoutCancellationTokenSource.Cancel();
            var result = await task;
            return result;
        }
        else
        {
            throw new TimeoutException("timeout");
        }
    }

    /// <summary>
    /// Task with timeout
    /// </summary>
    /// <param name="task"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public static async System.Threading.Tasks.Task TimeoutAfter(this System.Threading.Tasks.Task task, TimeSpan timeout)
    {
        using var timeoutCancellationTokenSource = new CancellationTokenSource();
        var completedTask = await System.Threading.Tasks.Task.WhenAny(task, System.Threading.Tasks.Task.Delay(timeout, timeoutCancellationTokenSource.Token));
        if (completedTask == task)
        {
            timeoutCancellationTokenSource.Cancel();
            await task;
        }
        else
        {
            throw new TimeoutException("timeout");
        }
    }

    /// <summary>
    /// Run and throw exception
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public static async System.Threading.Tasks.Task Run(Action action)
    {
        Exception exception = null;
        await System.Threading.Tasks.Task.Run(() =>
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
        });
        if (exception != null)
        {
            throw exception;
        }
    }

    /// <summary>
    /// Run and return exception
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public static async Task<Exception> SafeRun(Action action)
    {
        Exception exception = null;
        await System.Threading.Tasks.Task.Run(() =>
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
        });
        return exception;
    }
}
