 using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebSonette.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(String message)
        {
            await Clients.All.SendAsync("messageSonette", message);
        }
    }
}
