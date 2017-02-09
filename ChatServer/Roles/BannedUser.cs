using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Roles
{
    public class BannedUser : RoleBase
    {
        private ClientObject client;

        public BannedUser(ClientObject client)
        {
            Handlers = new IHandlerModule[] { new Logout(), new Info(), new MessageReadonly(), new Private() };
            this.client = client;
            TrackBlackList(client.Username);
        }


        public async Task TrackBlackList(string username)
        {
            DateTime timeLeft = BlackListProvider.GetDateTillBanDiscard(username);
            TimeSpan tmp = timeLeft.Subtract(timeLeft);
            await Task.Delay(tmp);
            client.Role = new User();
            client.SendMessage(ResponseConstructor.GetUnBannedNotification(username));
        }
    }

        
}
