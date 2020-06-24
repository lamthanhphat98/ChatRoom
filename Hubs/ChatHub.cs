using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Hubs
{
    public class ChatHub : Hub
    { 
        public String GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}
