using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Newtonsoft.Json;

namespace MultiRoomChatClient
{
    public static class RequestManager
    {
        
        public static void Login(string name)
        {

            Client.AddRequest(getLoginRequest("in", name));
        }

        public static void Logout()
        {
            Client.AddRequest(getLoginRequest("out", null));
        }

        private static string getLoginRequest(string cmd, object args)
        {
            return JsonConvert.SerializeObject(new RequestObject("login", cmd, args));
        }


        public static void SendMessage(string msg)
        {
            RequestObject reqObj = new RequestObject("msg", "msg", new ChatMessage(Client.Username, msg, DateTime.Now));
            Client.AddRequest(JsonConvert.SerializeObject(reqObj));

        }

        public static void SetActiveRoom(string room)
        {
            RequestObject reqObj = new RequestObject("msg", "active", room);
            Client.AddRequest(JsonConvert.SerializeObject(reqObj));
        }

        public static void LeaveRoom(string room)
        {
            RequestObject reqObj = new RequestObject("msg", "leave", room);
            Client.AddRequest(JsonConvert.SerializeObject(reqObj));
        }

        public static void CreateRoom(string roomName)
        {
            RequestObject reqObj = new RequestObject("room", "create", roomName);
            Client.AddRequest(JsonConvert.SerializeObject(reqObj));
        }

        public static void CloseRoom(string roomName)
        {
            RequestObject reqObj = new RequestObject("room", "close", roomName);
            Client.AddRequest(JsonConvert.SerializeObject(reqObj));
        }

        public static void RequestData()
        {
            RequestObject msg = new RequestObject("info","get",null);           
            Client.AddRequest(JsonConvert.SerializeObject(msg));
        }

        /*public static void RequestMessageList(ChatMessage last = null)
        {
            RequestObject msg = new RequestObject("room", "msg", null);
            if(last!= null)
            {
                msg.args = last.TimeStamp;
            }
            Client.AddRequest(JsonConvert.SerializeObject(msg));
        }*/

        public static void SendPrivateMessage(string username, ChatMessage msg)
        {
            RequestObject message = new RequestObject("private", username, msg);
            Client.AddRequest(JsonConvert.SerializeObject(message));
        }

        public static void AdminBan(string username, DateTime exp)
        {
            RequestObject message = new RequestObject("admin", "ban", new object[] { username, exp });
            Client.AddRequest(JsonConvert.SerializeObject(message));
        }

        public static void AdminBanEternal(string username)
        {
            RequestObject message = new RequestObject("admin", "ban", username);
            Client.AddRequest(JsonConvert.SerializeObject(message));
        }
        public static void AdminUnban(string username, ChatMessage msg)
        {
            RequestObject message = new RequestObject("admin", "ban", msg);
            Client.AddRequest(JsonConvert.SerializeObject(message));
        }
    }
}
