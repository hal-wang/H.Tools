using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;

namespace Hubery.Tools.Uwp.Controls.Message
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageList : PopupLayout
    {
        /// <summary>
        /// 
        /// </summary>
        public MessageList()
        {
            this.DefaultStyleKey = typeof(MessageList);
            Messages.CollectionChanged += Messages_CollectionChanged;
        }

        private void Messages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (Messages.Count > 0)
            {
                IsOpen = true;
            }
            else
            {
                IsOpen = false;
            }
        }


        internal ObservableCollection<MessageListItem> Messages
        {
            get { return (ObservableCollection<MessageListItem>)GetValue(MessagesProperty); }
        }

        internal static readonly DependencyProperty MessagesProperty =
            DependencyProperty.Register("Messages", typeof(ObservableCollection<MessageListItem>), typeof(MessageList), new PropertyMetadata(new ObservableCollection<MessageListItem>()));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="messageType"></param>
        /// <param name="duration"></param>
        public void ShowMessage(string content, MessageType messageType, int duration)
        {
            var item = new MessageListItem()
            {
                Duration = TimeSpan.FromMilliseconds(duration),
                MessageType = messageType,
                Text = content
            };
            Messages.Add(item);

            if (duration > 0)
            {
                DispatcherTimer timer = new DispatcherTimer()
                {
                    Interval = item.Duration
                };
                timer.Tick += (ss, ee) =>
                {
                    timer.Stop();
                    if (Messages.Contains(item)) Messages.Remove(item);
                };
                timer.Start();
            }
            else
            {
                item.CloseManualy += RemoveItem;
            }
        }

        private void RemoveItem(MessageListItem item)
        {
            item.CloseManualy -= RemoveItem;
            if (Messages.Contains(item)) Messages.Remove(item);
        }
    }
}
