using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class MessageReadonly : RoomObserverBase
    {
        /*public override bool Handle(ClientObject client, RequestObject request)
        {
            if (request.Module != "msg")
            {
                return false;
            }
            RoomObject room = null;
            switch (request.Cmd)
            {
                case "active":
                    object[] args = JsonConvert.DeserializeObject<object[]>((string)request.args);
                    room = Manager.FindRoom((string)args[0]);
                    DateTime since = default(DateTime);
                    if (args[1] != null)
                    {
                        //since = JsonConvert.DeserializeObject<DateTime>((string)args[1]);
                        since = (DateTime)args[1];
                    }
                    if (room != null)
                    {
                        room.AddListener(this);
                        //Active = room;
                        ChatMessage[] msgs = room.GetMessageHistorySince(since);
                        if (msgs.Length > 0)
                        {
                            RequestObject req = new RequestObject("msg", "active", msgs);
                            client.SendMessage(JsonConvert.SerializeObject(req));
                        }
                    }
                    break;
                case "leave":
                    room = Manager.FindRoom((string)request.args);
                    if (room != null)
                    {
                        room.RemoveListener(this);
                        //if (Active == room)
                        //{
                        //    Active = Manager.Host;
                        //}
                    }
                    break;
                default: break;
            }
            return true;
        }*/

        protected override void HandleMessage(ClientObject client, RequestObject request){}
    }
}
