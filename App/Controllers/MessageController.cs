using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace App
{
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IHubContext<DemoHub> _hubContext;
        public MessageController(IHubContext<DemoHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("/message")]
        public Task PostMessage(ChatMessage message)
        {
            // Do something
            return _hubContext.Clients.All.SendAsync("Send", message.Message);
        }
    }
}