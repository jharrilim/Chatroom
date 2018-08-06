using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebSocket.Server
{
    public class HomeController : Controller
    {
        public IActionResult Spa() => File("~/index.html", "text/html");
    }
}