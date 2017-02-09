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
                    DateTime ts1 = (DateTime)args[1];
                    //meSpan ts = (TimeSpan)args[1];
                    //TimeSpan ts = JsonConvert.DeserializeObject<TimeSpan>(args[1].ToString());
                    //TimeSpan ts = new TimeSpan();
                    BanUser(username, ts1);
                    break;
                case "unban":
                    UnBanUser((string)request.args);
                    break;
                default: break;
            }
            return true;
        }

        private void BanUser(string username, DateTime duration)
        {
            ClientObject user = Manager.FindClient(username);
            BlackListProvider.AppendRecord(username, duration);
            if (user == null || user.ToString() == "")
                return;
            user.SendMessage(ResponseConstructor.GetBannedNotification(duration));
            user.Role = new BannedUser(user);
        }

        private void UnBanUser(string username)
        {
            ClientObject user = Manager.FindClient(username);
            BlackListProvider.RemoveRecord(username);
            string tmp = ResponseConstructor.GetUnBannedNotification(username);
            user.SendMessage(tmp);
            user.Role = new User();
        }
    }
}
