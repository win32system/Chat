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
                client.SendMessage(JsonConvert.SerializeObject(new RequestObject("msg", null, new object[] { room, msg })));
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
            RoomObject room = null;
            switch (request.Cmd)
            {
                case "msg":
                    object[] args; //(object[])JsonConvert.DeserializeObject(request.args.ToString());
                    ChatMessage msg = args[1] as ChatMessage; //JsonConvert.DeserializeObject<ChatMessage>((string)request.args[0]);
                    RoomObject r = Manager.FindRoom((string)args[0]);
                    r.Broadcast(client, msg);
                    break;
                case "active":
                    args = JsonConvert.DeserializeObject<object[]>(request.args.ToString());
                    room = Manager.FindRoom((string)args[0]);
                    DateTime since = default(DateTime);
                    if (args[1] != null)
                    {
                        since = JsonConvert.DeserializeObject<DateTime>((string)args[1]);
                    }
                    if (room != null)
                    {
                        room.AddListener(this);
                        //Active = room;
                        ChatMessage[] msgs = room.GetMessageHistorySince(since);
                        if(msgs.Length > 0)
                        {
                            RequestObject req = new RequestObject("msg", "active", new object[] { room.Name, msgs });
                            client.SendMessage(JsonConvert.SerializeObject(req));
                        }
                    }
                    break;
                case "leave":
                    room = Manager.FindRoom((string)request.args);
                    //if (room != null)
                    //{
                        room.RemoveListener(this);
                       // if (Active == room)
                        //{
                       //     Active = Manager.Host;
                        //}
                    //}
                    break;
                default: break;
            }
            return true;
        }
    }
}
