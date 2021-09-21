using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace HTools.Uwp.Helpers
{

    internal class ContentDialogItem
    {
        public TaskCompletionSource<object> Awaiter;
        public ContentDialog Dialog;
        public Task ShowTask = null;
        public Type ResultType;
    }

    public static class DialogExtend
    {
        private static readonly List<ContentDialogItem> _dialogs = new();

        private static async Task ShowDialog(ContentDialogItem preDialog, ContentDialogItem nextDialog)
        {
            static async Task<ContentDialogResult> ShowDialog(ContentDialog dialog)
            {
                dialog.Closed += ActiveDialog_Closed;
                return await dialog.ShowAsync();
            }

            if (preDialog != null && preDialog.ShowTask != null)
            {
                await preDialog.ShowTask;
            }

            if (_dialogs.Count > 0 && _dialogs[0] != nextDialog)
            {
                // 防止多线程
                return;
            }

            nextDialog.ShowTask = ShowDialog(nextDialog.Dialog);
            new Action(async () => { await nextDialog.ShowTask; }).Invoke();
        }

        private static async Task<T> QueueAsync<T>(ContentDialog dialog, bool ahead, bool customResult)
        {
            if (!customResult && typeof(T) != typeof(ContentDialogResult))
            {
                throw new ArgumentException();
            }
            if (customResult && dialog is not IResultDialog<T>)
            {
                throw new ArgumentException();
            }

            ContentDialogItem dialogItem = new()
            {
                Awaiter = new TaskCompletionSource<object>(),
                Dialog = dialog,
                ResultType = typeof(T),
            };

            ContentDialogItem preDialog = null;
            if (ahead && _dialogs.Count > 0)
            {
                preDialog = _dialogs[0];
                preDialog.ShowTask = null;
                preDialog.Dialog.Closed -= ActiveDialog_Closed;
                preDialog.Dialog.Hide();
            }

            _dialogs.Insert(ahead ? 0 : _dialogs.Count, dialogItem);

            if (_dialogs[0] == dialogItem)
            {
                await ShowDialog(preDialog, dialogItem);
            }

            var result = await dialogItem.Awaiter.Task;
            _dialogs.Remove(dialogItem);
            if (_dialogs.Count > 0)
            {
                await ShowDialog(dialogItem, _dialogs[0]);
            }
            return (T)result;
        }

        public static async Task<ContentDialogResult> QueueAsync(this ContentDialog dialog, bool ahead = true)
        {
            return await QueueAsync<ContentDialogResult>(dialog, ahead, false);
        }

        public static async Task<T> QueueAsync<T>(this ContentDialog dialog, bool ahead = true)
        {
            return await QueueAsync<T>(dialog, ahead, true);
        }

        private static void ActiveDialog_Closed(ContentDialog dialog, ContentDialogClosedEventArgs args)
        {
            dialog.Closed -= ActiveDialog_Closed;

            ContentDialogItem dialogItem = _dialogs.FirstOrDefault(d => d.Dialog == dialog);
            if (dialogItem != default)
            {
                if (dialogItem.ResultType == typeof(ContentDialogResult))
                {
                    dialogItem.Awaiter.SetResult(args.Result);
                }
                else
                {
                    dialogItem.Awaiter.SetResult(dialog.GetType().GetProperty("Result").GetValue(dialog));
                }
            }
        }
    }
}
