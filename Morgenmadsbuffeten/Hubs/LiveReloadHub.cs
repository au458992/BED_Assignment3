using Microsoft.AspNetCore.SignalR;

namespace Morgenmadsbuffeten.Hubs
{
    public class LiveReloadHub : Hub
    {
        public async Task Reload()
        {
            await Clients.All.SendAsync("Reload");
        }
    }
}
