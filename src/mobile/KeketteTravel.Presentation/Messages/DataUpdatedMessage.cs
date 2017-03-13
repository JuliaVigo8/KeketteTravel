using System;
using MvvmCross.Plugins.Messenger;

namespace KeketteTravel.Presentation.Messages
{
    public class DataUpdatedMessage : MvxMessage
    {
        public DataUpdatedMessage(object sender) : base(sender)
        {
        }
    }
}
