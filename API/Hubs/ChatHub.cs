using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.AspNetCore.SignalR;

namespace DocumentTransformation.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.Others.SendAsync("ReceivedMessage",user, message);
        }
    }

    public class LogHub : Hub
    {
        public async Task EmitLogMessage(LoggerInfo info)
        {
            await Clients.Others.SendAsync("RecievedLogEvent", info);
        }
    }
}
