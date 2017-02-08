using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Roles
{
    public class Admin : RoleBase
    {
        public Admin()
        {
            Handlers = new IHandlerModule[] { new Logout(), new Info(), new Message(), new Private(), new Room(), new AdminModule() };
        }
    }
}
