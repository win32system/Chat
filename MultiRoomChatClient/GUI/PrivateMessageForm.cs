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
    public partial class PrivateMessageForm : Form
    {
        public PrivateMessageForm(string username)
        {
            InitializeComponent();
            Recipient = username;
            var h = Client.PrivateHistory.GetHistory(Client.Username + @"/" + Recipient);
            if(h!= null)
            {
                Messages.AddRange(h);
            }
        }

        string Recipient;
        List<ChatMessage> Messages = new List<ChatMessage>();

        private void btn_send_Click(object sender, EventArgs e)
        {
            string message = text_msg.Text;
            ChatMessage msg = new ChatMessage(Client.Username, message, DateTime.Now);
            if(message.Length > 0)
            {
                list_msg.Items.Add(msg);
                Client.PrivateHistory.AppendMessage(Client.Username + @"/" + Recipient, msg);
                RequestManager.SendPrivateMessage(Client.Username, msg);
            }
        }
    }
}
