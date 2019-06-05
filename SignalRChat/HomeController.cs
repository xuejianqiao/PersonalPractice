using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SignalRChat
{
    public class HomeController : Controller
    {
        private readonly IHubContext<ChatHub> _hubContext;
        public HomeController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            List<string> groups = new List<string>() { "SignalR Users" };
            await _hubContext.Clients.Groups(groups).SendAsync("ReceiveMessageByGroups", "测试后端发送");
            return View();
        }
    }
}
