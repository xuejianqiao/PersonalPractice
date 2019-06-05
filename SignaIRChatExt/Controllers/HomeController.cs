using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignaIRChatExt.Hubs;
using SignaIRChatExt.Models;

namespace SignaIRChatExt.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHubContext<ChatHub> _hubContext;
        public HomeController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult About()
        {
            List<string> groups = new List<string>() { "SignalR Users" };
            _hubContext.Clients.Groups(groups).SendAsync("ReceiveMessageByGroups", "来自后台！！");
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
