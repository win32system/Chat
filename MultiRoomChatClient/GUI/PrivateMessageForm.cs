using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiRoomChatClient
{
    public partial class  PrivateMessageForm : Form
    {
        public string Recipient;
        private List<ChatMessage> Messages;
        public PrivateMessageForm(string username)
        {
            InitializeComponent();
            Messages = new List<ChatMessage>();
            Recipient = username;
            Text = username;
            var h = Client.PrivateHistory.GetHistory(Client.Username + @"-" + Recipient);
            if(h!= null)
            {
                Messages.AddRange(h);
            }
        }

        public void AppendMessage(ChatMessage msg)
        {
            list_msg.Items.Add(msg);
            Client.PrivateHistory.AppendMessage(Client.Username + @"-" + Recipient, msg);
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            string message = text_msg.Text;
            ChatMessage msg = new ChatMessage(Client.Username, message, DateTime.Now);
            if(message.Length > 0)
            {
                AppendMessage(msg);
                RequestManager.SendPrivateMessage(Recipient, msg);
            }
        }
    }
}
