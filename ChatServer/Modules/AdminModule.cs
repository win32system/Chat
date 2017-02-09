using ChatServer.Roles;
using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class AdminModule : IHandlerModule
    {
        public bool Handle(ClientObject client, RequestObject request)
        {
            if (request.Module != "admin")
            {
                return false;
            }

            switch (request.Cmd)
            {
                case "ban":
                    object[] args = JsonConvert.DeserializeObject<object[]>(request.args.ToString());
                    string username = (string)args[0];
                    TimeSpan ts = JsonConvert.DeserializeObject<TimeSpan>((string)args[1]);
                    BanUser(username, ts);
                    break;
                case "unban":
                    UnBanUser((string)request.args);
                    break;
                default: break;
            }
            return true;
        }

        private void BanUser(string username, TimeSpan duration)
        {
            ClientObject user = Manager.FindClient(username);
            BlackListProvider.AppendRecord(username, duration);
            user.SendMessage(ResponseConstructor.GetBannedNotification(duration));
            user.Role = new BannedUser(user);
        }

        private void UnBanUser(string username)
        {
            ClientObject user = Manager.FindClient(username);
            BlackListProvider.RemoveRecord(username);
            user.SendMessage(ResponseConstructor.GetUnBannedNotification());
            user.Role = new User(user);
        }
    }
}
