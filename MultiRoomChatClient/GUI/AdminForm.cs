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
    public partial class AdminForm : SuperDuperChat
    {
        public AdminForm() : base()
        {
            InitializeComponent();
        }

      
        private void btn_banForever_Click(object sender, EventArgs e)
        {
          
       }

        private void button3_Click(object sender, EventArgs e)
        {
           // MessageBox.Show(dateTime.Value.ToString());
            string userName =  tb_selectedUser.Text.ToString();
            if (userName == null || userName == "")
                return;
               
            RequestManager.AdminBan(userName, dateTime.Value);
        }

        private void Unban_Click(object sender, EventArgs e)
        {
            string userName = tb_selectedUser.Text.ToString();
            if (userName == null || userName == "")
                return;

            RequestManager.AdminUnban(userName);
        }
    }
}
