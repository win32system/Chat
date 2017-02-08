using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Roles
{
    public class UnknownUser : RoleBase
    {
        public UnknownUser()
        {
            Handlers = new IHandlerModule[] { new Login() };
        }
    }
}
