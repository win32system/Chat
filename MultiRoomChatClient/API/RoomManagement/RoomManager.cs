using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiRoomChatClient
{
    public class RoomManager
    {
        public LinkedList<RoomObjExt> Rooms = new LinkedList<RoomObjExt>();

        public delegate void refreshDelegate();
        public event refreshDelegate MessageListUpdated;
        public event refreshDelegate RoomDataUpdated;

        public RoomManager()
        {
            HistoryDataprovider HistoryProvider = new HistoryDataprovider("Msg");  

            ResponseHandler.roomDataReceived += onRoomDataReceived;
            ResponseHandler.UserEntered += AddUser;
            ResponseHandler.UserLeft += RemoveUser;
            RequestManager.RequestData();
            ResponseHandler.roomCreated += (x) => AddRoom(new RoomObj(x));
            ResponseHandler.roomRemoved += (x) => RemoveRoom(new RoomObj(x));
        }

        private void RemoveUser(string room, string username)
        {
            FindRoom(room)?.clients.Remove(username);
            RoomDataUpdated?.Invoke();
        }

        private void AddUser(string room, string username)
        {
            FindRoom(room)?.clients.Add(username);
            RoomDataUpdated?.Invoke();
        }

        public RoomObjExt FindRoom(string name)
        {
            RoomObjExt room = null;
            foreach (RoomObjExt r in Rooms)
            {
                if(r.Name == name)
                {
                    room = r;
                    break;
                }
            }
            return room;
        }

        public void CreateRoom(string room)
        {
            RoomObjExt r = new RoomObjExt(room);
            AddRoom(r);
            RequestManager.CreateRoom(room);
            r.OnDataReceived(Client.RoomHistory.GetHistory(room));
            MessageListUpdated?.Invoke();
            RoomDataUpdated?.Invoke();
        }

        private void AddRoom(RoomObj room)
        {
            if (!Rooms.Contains(room))
            {
                Rooms.AddLast(new RoomObjExt(room));
                RoomDataUpdated?.Invoke();

            }
        }

        private void RemoveRoom(RoomObj room)
        {
            if (!Rooms.Contains(room))
            {
                Rooms.Remove(new RoomObjExt(room));
                RoomDataUpdated?.Invoke();
            }
        }

        private void onRoomDataReceived(RoomObj[] rooms)
        {
            Rooms.Clear();
            foreach (RoomObj room in rooms)
            {
                Rooms.AddLast(new RoomObjExt(room));
            }
            RoomDataUpdated?.Invoke();
        }

        //public void MoveTo(RoomObjExt room)
        //{
        //   /* if (Active != room)
        //    {
        //        Active = room;
        //        RequestManager.MoveToRoom(room.Name);
        //    }

        //    LoadMessageHistory();
        //    RequestUpdateMessageList();*/
        //    MessageListUpdated?.Invoke();
        //    RoomDataUpdated?.Invoke();
        //}

        /**/

        //public void RemoveRoom(RoomObj room)
        //{
        //    RoomObjExt temp = null;
        //    foreach (RoomObjExt r in Rooms)
        //    {
        //        if (r.Equals(room))
        //        {
        //            temp = r;
        //            break;
        //        }
        //    }
        //    Rooms.Remove(temp);
        //}

        /*public void MoveClient(string from, string to, string p)
        {
            if (from != null)
            {
                foreach(RoomObjExt r in Rooms)
                {
                    if(r.Name == from)
                    {
                        r.clients.Remove(p);
                    }
                }
            }
            bool found = false;
            foreach (RoomObjExt room in Rooms)
            {
                if (room.Name == to)
                {
                    room.clients.Add(p);
                    if(p.id == Client.ClientObject.id)
                    {
                        Active = room;
                    }
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                RoomObjExt nRoom = new RoomObjExt(to);
                nRoom.clients.Add(p);
                Rooms.AddLast(nRoom);
            }

            RoomDataUpdated?.Invoke();
        }*/
        /*
        private void RequestUpdateMessageList()
        {
            ChatMessage last = null;
            if(Active.Messages.Count > 0)
            {
                last = Active.Messages[Active.Messages.Count - 1];
            }
            RequestManager.RequestMessageList(last);
        }
        private void LoadMessageHistory()
        {
            ChatMessage[] msgs = null;
            if (Active.Messages.Count == 0)
            {
                msgs = HistoryDataprovider.GetHistory(Active);
            }
            else
            {
                msgs = HistoryDataprovider.GetHistory(Active, Active.Messages.Last());
            }
            
            if(msgs != null)
            {
                Active.Messages.AddRange(msgs);
            }
            MessageListUpdated.Invoke();
        }*/

        /*public void AddMessage(ChatMessage msg)
        {
            Active.Messages.Add(msg);
            HistoryDataprovider.AppendMessage(Active.Name, msg);
            MessageListUpdated?.Invoke();
        }
        */
        /*
        public void AppendMessages(ChatMessage[] msgs)
        {
            Active.Messages.AddRange(msgs);
            HistoryDataprovider.AppendSequence(Active, msgs);
            MessageListUpdated?.Invoke();
        }
        */


    }
}
