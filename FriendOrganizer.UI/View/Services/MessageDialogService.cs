using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FriendOrganizer.UI.View.Services
{
    public class MessageDialogService : IMessageDialogService
    {

        public MessageDialogResult ShowOkCancel(string message, string title)
        {
            var result = MessageBox.Show(message, title, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
                return MessageDialogResult.OK;
            return MessageDialogResult.Cancel;
        }

    }
    public enum MessageDialogResult
    {
        OK, 
        Cancel 
    }
}
