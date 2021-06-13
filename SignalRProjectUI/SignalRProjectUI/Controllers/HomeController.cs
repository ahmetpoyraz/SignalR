using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SignalRProjectUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace SignalRProjectUI.Controllers
{
    public class HomeController : Controller
    {
        Context db = new Context();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
           
            _logger = logger;
        }

        public IActionResult Index()
        {
           
            return View();
        }
       
        public IActionResult Privacy()
        {
            var result = HttpContext.User.Claims.ToList();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       [HttpPost]
        public JsonResult Data(string data)
        {
           var result =  db.ActiveUsers.ToList();


            return Json(result);
        }
    }
}
