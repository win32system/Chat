using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Roles
{
    public class BannedUser : RoleBase
    {

        public BannedUser(ClientObject client): base(client)
        {
            MessageReadonly ro = new MessageReadonly();
            ro.client = this.client;
            Handlers = new IHandlerModule[] {
                new Logout(),
                new Info(),
                ro,
                new Private() };
            this.client = client;
            TrackBlackList(client.Username);
        }


        public async Task TrackBlackList(string username)
        {
            DateTime timeLeft = BlackListProvider.GetDateTillBanDiscard(username);

            await Task.Delay(timeLeft);
            client.Role = new User(client);
            client.SendMessage(ResponseConstructor.GetUnBannedNotification());
        }
    }

        
}
