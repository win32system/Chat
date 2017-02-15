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

        public LinkedList<PrivateMessageForm> PMForms = new LinkedList<PrivateMessageForm>();
        public delegate void tree_user(string name);
        public event tree_user treename;

        public SuperDuperChat()
        {
            InitializeComponent();
            Manager = new RoomManager();
            Manager.RoomDataUpdated += () => Invoke(new Action(onRoomDataUpdated));
            ResponseHandler.Banned += () => Invoke(new Action(Ban));
            ResponseHandler.Unbanned += () => Invoke(new Action(unBan));
            ResponseHandler.privateMessageReceived += (x) => Invoke(new Action<ChatMessage>(HandleMessage), x);
            ResponseHandler.roomError += (x) => Invoke(new Action<string>(OnRoomError), x);
        }

        public void HandleMessage(ChatMessage msg)
        {
            string sender = msg.Sender;
            PrivateMessageForm roomToHandle = null;
            foreach (PrivateMessageForm f in PMForms)
            {
                if (f.Recipient == sender)
                {
                    roomToHandle = f;
                }
            }
            if (roomToHandle == null)
            {
                roomToHandle = new PrivateMessageForm(sender, this);
                PMForms.AddLast(roomToHandle);
                roomToHandle.Show();
            }
            roomToHandle.AppendMessage(msg);
            roomToHandle.BringToFront();
        }

        public void PMFormRemove(PrivateMessageForm PMForm)
        {
            PMForms.Remove(PMForm);
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            string msg = tb_message.Text;
            if(msg.Length > 0)
            {
                tabbedMessageList1.SendMessage(msg);   
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
            }
        }

        private void OnRoomError(string error)
        {
            MessageBox.Show(error);
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

        private void btn_createRoom_Click(object sender, EventArgs e)
        {
            string newRoom = tb_newRoom.Text;
            if (newRoom.Length > 0)
            {
                Manager.CreateRoom(newRoom);
            }
            tb_newRoom.Clear();
        }

        public void Ban()
        {
            btn_send.Enabled = false;
            tb_message.Enabled = false;
            btn_createRoom.Enabled = false;
        }
        public void unBan()
        {
            btn_send.Enabled = true;
            tb_message.Enabled = true;
            btn_createRoom.Enabled = true;
        }
        
        private void tree_Room_MouseDoubleClick(object sender, EventArgs e)
        {
            if (tree_Room.SelectedNode == null)
                return;

            var tag = tree_Room.SelectedNode.Tag;
            if (tag is RoomObjExt)
            {
                tabbedMessageList1.AddRoom(tag as RoomObjExt);
            }
            else if ((tag is string) && tag.ToString() != Client.Username.ToString())
            {
                PrivateMessageForm PmForm = new PrivateMessageForm(tag as string,this);
                PMForms.AddLast(PmForm);
                this.treename?.Invoke(tag.ToString());
                PmForm.Show();
            }
        }
        private void btn_closeRoom_Click(object sender, EventArgs e)
        {
            tabbedMessageList1.CloseRoom();
        }

        protected void SuperDuperChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            tabbedMessageList1.CloseAllRooms();
            Manager.RoomDataUpdated -= () => Invoke(new Action(onRoomDataUpdated));
            ResponseHandler.Banned -= () => Invoke(new Action(Ban));
            ResponseHandler.Unbanned -= () => Invoke(new Action(unBan));
            RequestManager.Logout();
            Client.Disconnect();
        }

       
    }
}
