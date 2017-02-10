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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            ResponseHandler.loginSuccessfull += (x) => Invoke(new Action<string>(On_LoginSuccessfull), x);
            ResponseHandler.loggedAsAdmin    += (x) => Invoke(new Action<string>(On_LoginAdmin), x);
            ResponseHandler.loggedBanned     += (x) => Invoke(new Action<string>(On_LoginBanned), x) ;
            ResponseHandler.loginFail        += (x) => Invoke(new Action(On_LoginFailed));
        }

        public LinkedList<PrivateMessageForm> PmForms = new LinkedList<PrivateMessageForm>();

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if (login_box.Text == null || login_box.Text == "")
                return;

            Client.StartClient();
            RequestManager.Login(login_box.Text);
        }
        private void On_LoginFailed()
        {
            login_box.BackColor = Color.Coral;
        }
        
        private SuperDuperChat On_Log(string UserName)
        {
            Client.Username = UserName;
            var chat = new SuperDuperChat();
            chat.Location = Location;
            chat.StartPosition = StartPosition;
            chat.FormClosing += (x, y) => this.Show();
            chat.Show();
            this.Hide();
            return chat;
        }

        private void On_LoginSuccessfull(string Username)
        {
            On_Log(Username);
        }

        private void On_LoginBanned(string Username)
        {
            var chat = On_Log(Username);
            chat.Ban();
        }
        
        private void On_LoginAdmin(string Username)
        {
            Client.Username = Username;
            var chat = new AdminForm();
            chat.Location = Location;
            chat.StartPosition = StartPosition;
            chat.FormClosing += (x, y) => this.Show();
            chat.Show();
            this.Hide();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
