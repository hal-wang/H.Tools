using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace HTools.Uwp.Helpers
{

    internal class ContentDialogItem
    {
        public TaskCompletionSource<ContentDialogResult> Awaiter;
        public ContentDialog Dialog;
        public Task ShowTask = null;
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

        public static async Task<ContentDialogResult> QueueAsync(this ContentDialog dialog, bool ahead = true)
        {
            ContentDialogItem dialogItem = new()
            {
                Awaiter = new TaskCompletionSource<ContentDialogResult>(),
                Dialog = dialog,
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

            ContentDialogResult result = await dialogItem.Awaiter.Task;
            _dialogs.Remove(dialogItem);
            if (_dialogs.Count > 0)
            {
                await ShowDialog(dialogItem, _dialogs[0]);
            }
            return result;
        }

        private static void ActiveDialog_Closed(ContentDialog dialog, ContentDialogClosedEventArgs args)
        {
            dialog.Closed -= ActiveDialog_Closed;

            ContentDialogItem dialogItem = _dialogs.FirstOrDefault(d => d.Dialog == dialog);
            if (dialogItem != default)
            {
                dialogItem.Awaiter.SetResult(args.Result);
            }
        }
    }

}
