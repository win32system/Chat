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
      public int getCountRoom()
        {
            return this.tabControl1.TabPages.Count;
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
            TabPage tp = new TabPage(room.Name);
            tp.Tag = room;
            ListBox lb = new ListBox();
            
            lb.Dock = DockStyle.Fill;
            lb.DataSource = room.Messages;
            room.MessageReceived += (x) => {
                /////kostyl
                lb.Invoke(new Action(() => {
                    lb.DataSource = null;
                    lb.DataSource = room.Messages;
                    int visibleItems = lb.ClientSize.Height / lb.ItemHeight;
                    lb.TopIndex = Math.Max(lb.Items.Count - visibleItems + 1, 0);
                    
                    //tp.Text += room.Name1;
                }));
            };
            room.NotificationUpdated += (x) =>
            {
                tp.Text = x > 0 ? room.Name : room.Name + " (" + x + ")";
            };
            tabControl1.SelectedTab = tp;
            tp.Controls.Add(lb);
         
            room.Bind();
            room.SetActive();
            this.tabControl1.TabPages.Add(tp);
        }

        public void CloseRoom()
        {
            var tab = this.tabControl1.SelectedTab;
            if (tab == null || tab.ToString() == "")
                return;
            this.tabControl1.TabPages.Remove(tab);
            RequestManager.LeaveRoom(((RoomObjExt)(tab.Tag)).Name);
            RoomObjExt room = tab.Tag as RoomObjExt;
            room.Unbind(); 
        }

        public void CloseAllRooms()
        {
            while(tabControl1.TabPages.Count > 0)
            {
                CloseRoom();
            }
        }

        public void SendMessage(string msg)
        {
            var tab = tabControl1.SelectedTab;
            if (tab == null || tab.ToString() == "")
                return;
            RoomObjExt current = (RoomObjExt)tab.Tag;
            current.SendMessage(msg);
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            
        }
    }
}
