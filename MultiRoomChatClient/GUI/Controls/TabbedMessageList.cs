using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiRoomChatClient
{
    public partial class TabbedMessageList : UserControl
    {
        public TabbedMessageList()
        {
            InitializeComponent();
        }
      
        public void SelectActive(RoomObjExt room)
        {
            var tab = this.tabControl1.SelectedTab;
            (tab.Tag as RoomObjExt).SetActive();
        }

        public void AddRoom(RoomObjExt room)
        {
            foreach (TabPage tmp in this.tabControl1.TabPages)
            {
                if(tmp.Tag == room)
                {
                    return;
                }
            }
            //this.tabControl1 = new TabControl();
            TabPage tp = new TabPage(room.Name);
            tp.Tag = room;
            ListBox lb = new ListBox();
            lb.Dock = DockStyle.Fill;
            lb.DataSource = room.Messages;
            room.MessageReceived += (x) => {
                /////kostyl
                lb.DataSource = null;
                lb.DataSource = room.Messages;
            };
            room.NotificationUpdated += (x) =>
            {
                tp.Text = x > 0 ? room.Name : room.Name + " (" + x + ")";
            };
            tp.Controls.Add(lb);
            room.SetActive();
            this.tabControl1.TabPages.Add(tp);
        }

        public void CloseRoom()
        {
            var tab = this.tabControl1.SelectedTab;
            this.tabControl1.TabPages.Remove(tab);
            RoomObjExt room = tab.Tag as RoomObjExt;
            room.Unbind();
        }

        public void SendMessage(string msg)
        {

        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            
        }
    }
}
