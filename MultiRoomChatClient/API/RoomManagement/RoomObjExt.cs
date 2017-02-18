﻿using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiRoomChatClient
{
    public class RoomObjExt: RoomObj
    {
        public delegate void messageDel(ChatMessage message);
        public delegate void notificationDel(int count);

        public event messageDel MessageReceived;
        public event notificationDel NotificationUpdated;
        public bool active;


        public RoomObjExt(RoomObj r)
        {
            this.Name = r.Name;
            this.clients = r.clients;
            ChatMessage[] msgs = Client.RoomHistory.GetHistory(Name);
            if(msgs!= null)
            {
                foreach(ChatMessage msg in msgs)
                {
                    Messages.Add(msg);
                }
            }
        }

        public void SendMessage(string message)
        {
            ChatMessage msg = new ChatMessage("Me", message, DateTime.Now);
            
            RequestManager.SendMessage(message, this.Name);
            Messages.Add(msg);
            Client.RoomHistory.AppendMessage(Name, msg);
            if (active)
            {
                MessageReceived?.Invoke(msg);
          
            }
        }

        public void OnMessageReceived(string room, ChatMessage msg)
        {
            if(room != Name)
            {
                return;
            }
            Messages.Add(msg);
            Client.RoomHistory.AppendMessage(Name, msg);

            //if ( active)
           // {    
            MessageReceived?.Invoke(msg);

            //TabbedMessageList.roomName += (x) =>
            //{
            //    if (x != room)
            //    {
            //        Notifications++;
            //        NotificationUpdated?.Invoke(Notifications);
            //    }
            //};

            
              //  
            
            //}
            //else
            //{
            //     Notifications++;
            //     NotificationUpdated?.Invoke(Notifications);
            // }
        }

        public void SetActive()
        {
            object d = null;
            if(Messages.Count > 0)
            {
                d = Messages.Last().TimeStamp;
            }
            object[] args = new object[] { Name, d };
            RequestManager.SetActiveRoom(Name, args);
            active = true;
            Notifications = 0;
        }

        public void Bind()
        {
            ResponseHandler.messageRecieived += HandleMessage;
        }

        public void Unbind()
        {
            RequestManager.LeaveRoom(Name);
            ResponseHandler.messageRecieived -= HandleMessage;
            active = false;
            Notifications = 0;
        }
       

        private void HandleMessage(string room, ChatMessage msg)
        {
            if(room != Name)
            {
                return;
            }
            OnMessageReceived(room, msg);
        }

        //public void Suspend()
        //{
        //    active = false;
        //}

        public void OnDataReceived(ChatMessage[] msg)
        {
            Messages.AddRange(msg);
            Client.RoomHistory.AppendSequence(Name, msg);
        }

        public RoomObjExt(string Name): base(Name){}

      //  public RoomObjExt() : base(){}

        public List<ChatMessage> Messages = new List<ChatMessage>();

        public int Notifications { get; set; }
    }
}
