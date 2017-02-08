using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Roles
{
    public class User : RoleBase
    {
        public User()
        {
            Handlers = new IHandlerModule[] { new Logout(), new Info(), new Message(), new Private(), new Room() };
        }
    }
}
