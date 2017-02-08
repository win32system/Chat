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
            ResponseHandler.loggedAsAdmin += (x) => Invoke(new Action<string>(On_LoginAdmin), x);
            ResponseHandler.loggedBanned += (x) => Invoke(new Action<string>(On_LoginBanned), x);
            ResponseHandler.loginFail        += (x)  => Invoke(new Action(On_LoginFailed));
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            string name = login_box.Text;
            Client.StartClient();
            RequestManager.Login(name);
        }
        private void On_LoginFailed()
        {
            login_box.BackColor = Color.Coral;
        }

        private void On_LoginSuccessfull(string Username)
        {
            Client.Username = Username;
            var chat = new SuperDuperChat();
            chat.Location = Location;
            chat.StartPosition = StartPosition;
            chat.FormClosing += (x, y) => this.Show();
            chat.Show();
            this.Hide();
        }

        private void On_LoginBanned(string Username)
        {
            Client.Username = Username;
            var chat = new SuperDuperChat();
            chat.Location = Location;
            chat.StartPosition = StartPosition;
            chat.FormClosing += (x, y) => this.Show();
            chat.Show();
            chat.Ban();
            this.Hide();
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
