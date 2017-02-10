using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiRoomChatClient
{
    public class PrivateManager
    {
        public LinkedList<PrivateMessageForm> PMForms = new LinkedList<PrivateMessageForm>();

        public PrivateManager()
        {
            ResponseHandler.privateMessageReceived += HandleMessage;            
        }
        

        public void HandleMessage(ChatMessage msg)
        {
            string sender = msg.Sender;
            PrivateMessageForm roomToHandle = null;
            foreach(PrivateMessageForm f in PMForms)
            {
                if(f.Recipient == sender)
                {
                    roomToHandle = f;
                }
            }
            if(roomToHandle == null)
            {
                roomToHandle = new PrivateMessageForm(sender);
                PMForms.AddLast(roomToHandle);
                roomToHandle.Show();
            }
            else
            {
                roomToHandle.AppendMessage(msg);
            }
            roomToHandle.BringToFront();
        }

        public void NewPrivateMessageForm(string username)
        {           
            var form = new PrivateMessageForm(username);

            ResponseHandler.privateMessageReceived += (msg) =>
            {
                PrivateMessageForm PmForm = new PrivateMessageForm(msg.Sender);
                PmForm.Show();
            };
        }
    }
}
