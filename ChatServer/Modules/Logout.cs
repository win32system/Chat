using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class Logout : IHandlerModule
    {
        public bool Handle(ClientObject client, RequestObject request)
        {
            if (request.Module != "logout")
            {
                return false;
            }
           
            foreach(RoomObject r in Manager.Rooms)
            {
                r.BroadcastAll(ResponseConstructor.GetUserLeftNotification(r.Name, client.Username));
            }
            client.Close();
            
            return true;
        }
    }
}
