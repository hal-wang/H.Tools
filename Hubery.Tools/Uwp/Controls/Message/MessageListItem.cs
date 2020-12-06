using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;

namespace Hubery.Tools.Uwp.Controls.Message
{
    /// <summary>
    /// 
    /// </summary>
    internal class MessageListItem : IMessage
    {
        /// <summary>
        /// 
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MessageType MessageType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool AutoHide => Duration != default;

        public Action<MessageListItem> CloseManualy;

        private ICommand _closeCommand = null;
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(() =>
                      {
                          CloseManualy?.Invoke(this);
                      });
                }
                return _closeCommand;
            }
        }
    }
}
