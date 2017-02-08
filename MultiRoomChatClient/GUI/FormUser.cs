using Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiRoomChatClient
{
    public partial class SuperDuperChat : Form
    {
        RoomManager Manager;

        public SuperDuperChat()
        {
            InitializeComponent();
            Manager = new RoomManager();
            Manager.RoomDataUpdated += () => Invoke(new Action(onRoomDataUpdated));
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            string msg = tb_message.Text;
            if(msg.Length > 0)
            {
                RequestManager.SendMessage(msg);
             //   Manager.AddMessage(new ChatMessage(Client.ClientObject, msg, DateTime.Now) );
              //  kostylRefreshList();
            }

            tb_message.Clear();
        }

        private void onRoomDataUpdated()
        {
            tree_Room.Nodes.Clear();
            foreach (RoomObjExt room in Manager.Rooms)
            {
                TreeNode roomNode = RoomToTreeNode(room);
                tree_Room.Nodes.Add(roomNode);
                //tree_Room.Nodes.Add(roomNode);
                /*     if(Manager.Active == room)
                     {
                         roomNode.BackColor = Color.Blue;
                     }*/
            }
        }

        private TreeNode RoomToTreeNode(RoomObjExt room)
        {
            TreeNode roomNode = new TreeNode(room.Name);
            roomNode.Tag = room;
            foreach(string p in room.clients)
            {
                TreeNode clnt = new TreeNode(p);
                clnt.Tag = p;
                roomNode.Nodes.Add(clnt);
            }
            return roomNode;
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            RequestManager.Logout();
            this.Close();
        }

        private void btn_createRoom_Click(object sender, EventArgs e)
        {
            string newRoom = textBox_newRoom.Text;
            if (newRoom.Length > 0)
            {
                Manager.CreateRoom(newRoom);
            }
            textBox_newRoom.Clear();
        }

        public void Ban()
        {
            btn_send.Enabled = false;
            tb_message.Enabled = false;
            btn_createRoom.Enabled = false;
        }
        TabbedMessageList tab = new TabbedMessageList();
        private void tree_Room_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // MessageBox.Show(tree_Room.SelectedNode.Text);
            //    var tag = tree_Room.SelectedNode.Tag;
            //    if (tag is RoomObjExt)
            //    {
            //        Manager.FindRoom(tree_Room.SelectedNode.Text);
            ////        PrivateMessageForm PmForm = new PrivateMessageForm() 
            ////PrivateMessageForm 
            ////}
            //else if(tag is string)
            //{
            //    PrivateMessageForm pmForm = new PrivateMessageForm("lil"); //tag as string);
            //    pmForm.Show();
            //}
            var tag = tree_Room.SelectedNode.Tag;
            if (tag is RoomObjExt)
            {
               
                tab.AddRoom(tag as RoomObjExt);
                //Manager. AddRoom(tag as RoomObjExt);//  CreateRoom(  MoveTo( tag as RoomObjExt);
            }
            else if (tag is string)
            {
                PrivateMessageForm PmForm = new PrivateMessageForm(tag as string);
                PmForm.Show();
            }
        }
    }
}
