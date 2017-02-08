using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class Room : IHandlerModule
    {
        public bool Handle(ClientObject client, RequestObject request)
        {
            if(request.Module != "room")
            {
                return false;
            }

            if(request.Cmd == "create")
            {
                Manager.CreateRoom((string)request.args);
            }
            else if (request.Cmd == "close")
            {
                Manager.CloseRoom((string)request.args);
            }
            return true;
        }
    }
}
