using Microsoft.AspNetCore.SignalR;
using Students_Angular_App.Common.Dtos;
using System.Threading.Tasks;

namespace Student_Angular_App.BLL.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(MessageDto message)
        {            
            await Clients.All.SendAsync("Send message", message);
        }

        public override Task OnConnectedAsync()
        {
            
            return base.OnConnectedAsync();
        }
    }
}
