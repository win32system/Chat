using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public static class Manager
    {
        public static RoomObject Host { get; set; }
        public static LinkedList<RoomObject> Rooms = new LinkedList<RoomObject>();
        public static LinkedList<ClientObject> Clients = new LinkedList<ClientObject>();

        public static ClientObject FindClient(string Name)
        {
            foreach (ClientObject client in Clients)
            {
                if (client.Username == Name)
                {
                    return client;
                }
            }
            return null;
        }

        public delegate void RoomDelegate(string roomName);
        public static event RoomDelegate RoomCreated;
        public static event RoomDelegate RoomDeleted;
        
        public static RoomObject FindRoom(string name)
        {
            if(name == null || name == "Host")
            {
                return Host;
            }

            foreach(RoomObject r in Rooms)
            {
                if(r.Name == name)
                {
                    return r;
                }
            }
            return null;
        }

        public static void CreateRoom(string roomName)
        {
            if(FindRoom(roomName) == null)
            {
                RoomObject room = new RoomObject(roomName);
                Rooms.AddLast(room);
                RoomCreated?.Invoke(roomName);
                room.NewMessage += HistoryDataprovider.AppendMessage;
            }
        }

        public static void CloseRoom(string roomName)
        {
            RoomObject room = FindRoom(roomName);
            if(room != null )
            {
                Rooms.Remove(room);
                room.NewMessage -= HistoryDataprovider.AppendMessage;
            }
            RoomDeleted?.Invoke(roomName);
        }

        public static void BroadcastAll(string message)
        {
            foreach(ClientObject client in Clients)
            {
                client.SendMessage(message);
            }
        }

        public static void OnClientAdded(string room, string username)
        {
            BroadcastAll(ResponseConstructor.GetUserEnteredNotification(room, username));
        }

        public static void OnClientLeft(string room, string username)
        {
            BroadcastAll(ResponseConstructor.GetUserLeftNotification(room, username));
        }

        public static RoomObj[] GetAllInfo()
        {
            List<RoomObj> info = new List<RoomObj>();
            foreach(RoomObject room in Rooms)
            {
                info.Add(room.GetRoomObj());
            }
            return info.ToArray();
        }
    }
}
