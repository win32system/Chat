﻿using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChatServer
{
    public abstract class RoomObserverBase: IHandlerModule
    {
        public ClientObject client;
        //protected RoomObject Active;


        public virtual void On_MessageReceived(string room, ChatMessage msg)
        {
            if(msg.Sender == client.Username)
            {
                return;
            }

            //if(Active.Name == room)
            //{
                client.SendMessage(JsonConvert.SerializeObject(new RequestObject("msg", "msg", new object[] { room, msg })));
            //}
            //else
            //{
           //     client.SendMessage(JsonConvert.SerializeObject(new RequestObject("msg", "notify", room)));
            //}
        }

        public void SendMessage(string msg)
        {
            client.SendMessage(msg);
        }

        public virtual bool Handle(ClientObject client, RequestObject request)
        {
            if(request.Module != "msg")
            {
                return false;
            }
            
            switch (request.Cmd)
            {
                case "msg":
                    HandleMessage(client, request);
                    break;
                case "active":
                    HandleActive(client, request);
                    break;
                case "leave":
                    HandleLeave(client, request);
                    break;
                default: break;
            }
            return true;
        }


        protected virtual void HandleMessage(ClientObject client, RequestObject request)
        {
            object[] args = JsonConvert.DeserializeObject<object[]>(request.args.ToString());
            string rstr = args[0] as string;
            ChatMessage msg = JsonConvert.DeserializeObject<ChatMessage>(args[1].ToString());
            RoomObject r = Manager.FindRoom(rstr);
            r?.Broadcast(client, msg);
        }

        protected virtual void HandleActive(ClientObject client, RequestObject request)
        {
            RoomObject room = null;
            object[] args = JsonConvert.DeserializeObject<object[]>(request.args.ToString());
            room = Manager.FindRoom((string)args[0]);
            DateTime since = default(DateTime);
            if (args[1] != null)
            {
                since = (DateTime)args[1];
            }
            if (room != null)
            {
                room.AddListener(this);
                //Active = room;
                ChatMessage[] msgs = room.GetMessageHistorySince(since);
                if (msgs.Length > 0)
                {
                    RequestObject req = new RequestObject("msg", "active", new object[] { room.Name, msgs });
                    client.SendMessage(JsonConvert.SerializeObject(req));
                }
            }
        }

        protected virtual void HandleLeave(ClientObject client, RequestObject request)
        {
            RoomObject room = Manager.FindRoom((string)request.args);
            //if (room != null)
            //{
            room.RemoveListener(this);
            // if (Active == room)
            //{
            //     Active = Manager.Host;
            //}
            //}
        }
    }
}
