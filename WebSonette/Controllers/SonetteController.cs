using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebSonette.Controllers
{
    //Creation d'une route pour ecouter les message nommee sonette
    [ApiController]
    [Route("sonette")]
    public class SonetteController : ControllerBase
    {

        private readonly IHubContext<Hubs.ChatHub> _chatHub;
        private readonly ILogger<SonetteController> _logger;

        public SonetteController(ILogger<SonetteController> logger, IHubContext<Hubs.ChatHub> chatHub)
        {
            _logger = logger;
            _chatHub = chatHub;
        }

        //Definition du Get
        [HttpGet]
        public async Task<string> GetAsync()
        {
            //Creer et envoi un signal avec un message lors de la reception d'un message de type GET
            string message = "Ca sonne !";
            await _chatHub.Clients.All.SendAsync("messageSonette", message);
            return "sonette";
        }
    }
}
