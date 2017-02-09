﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Roles
{
    public class User : RoleBase
    {
        public User(ClientObject clnt): base(clnt)
        {
            RoomObserver messageModule = new RoomObserver();
            messageModule.client = clnt;

            Handlers = new IHandlerModule[] {
                messageModule,
                new Logout(),
                new Info(),
                new Private(),
                new Room() };
        }
    }
}
