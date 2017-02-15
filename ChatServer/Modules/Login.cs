using ChatServer.Roles;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class Login : IHandlerModule
    {
        public bool Handle(ClientObject client, RequestObject request)
        {
            if(request.Module != "login")
            {
                return false;
            }

            string username = (string)request.args;

            if(Manager.FindClient(username) != null)
            {
                client.SendMessage(ResponseConstructor.GetErrorNotification("User with this username already exists", "login"));
                return true;
            }


            client.Username = username;
            if (IsAdmin(username))
            {
                client.Role = new Admin(client);
                client.SendMessage(ResponseConstructor.GetLoginResultNotification("admin", username));
            }
            else if (IsInBlackList(username))
            {
                client.Role = new BannedUser(client);
                client.SendMessage(ResponseConstructor.GetLoginResultNotification("banned", username));
            }
            else
            {
                client.Role = new User(client);
                client.SendMessage(ResponseConstructor.GetLoginResultNotification("ok", username));
            }
            
            return true;
        }

        private bool IsInBlackList(string Username)
        {
            return BlackListProvider.RecordExists(Username);
        }

        private bool IsAdmin(string Username)
        {
            if(Username == "admin")
            {
                return true;
            }
            return false;
        }
    }
}
