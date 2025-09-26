using System.Windows;
using System.Windows.Controls;

namespace H.Tools.Wpf;

public static class TextBoxExtend
{
    public static void RegisterTextChanged(this TextBox textBox, Func<Task> task, int delay = 200)
    {
        bool isHandleing = false;
        bool exitWaiting = false;

        async void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (isHandleing)
            {
                exitWaiting = true;
                return;
            }

            isHandleing = true;
            try
            {
                await task();
                await Task.Delay(delay);
            }
            finally
            {
                isHandleing = false;
                if (exitWaiting)
                {
                    exitWaiting = false;
                    OnTextChanged(sender, e);
                }
            }
        }

        void OnLostFocus(object sender, RoutedEventArgs e)
        {
            textBox.LostFocus -= OnLostFocus;
            textBox.TextChanged -= OnTextChanged;
        }

        textBox.LostFocus += OnLostFocus;
        textBox.TextChanged += OnTextChanged;
    }
}
