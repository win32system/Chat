﻿using Core;
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
            ResponseHandler.Banned += () => Invoke(new Action(Ban));
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
      
        private void tree_Room_MouseDoubleClick(object sender, EventArgs e)
        {

            if (tree_Room.SelectedNode == null)
                return;

            var tag = tree_Room.SelectedNode.Tag;
            if (tag is RoomObjExt)
            {
                tabbedMessageList1.AddRoom(tag as RoomObjExt);
            }
            else if (tag is string)
            {
                PrivateMessageForm PmForm = new PrivateMessageForm(tag as string);
                PmForm.Show();
            }
        }

        public delegate void tree_user(string name);
        public event tree_user treename;
        private void tree_Room_MouseClick(object sender, MouseEventArgs e)
        {
            var tree = tree_Room.SelectedNode;
            if (tree == null || !(tree.Tag is string))
                return;

            this.treename.Invoke(tree.Tag.ToString());
        }
    }
}
