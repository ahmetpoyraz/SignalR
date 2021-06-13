using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalRProjectUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SignalRProjectUI.Controllers
{
    public class ChatController : Controller
    {
        Context db = new Context();


        [Authorize]
        public IActionResult Index()
        {
            var activeUsersList = db.ActiveUsers.ToList();



            return View(activeUsersList);
        }

        [HttpPost]
        public JsonResult GetMessage(string data)
        {
            var authenticatedUser = User.Identity.Name;

            var result = db.Messages.Where(x => x.Receiver == data && x.Sender == authenticatedUser || x.Receiver == authenticatedUser && x.Sender == data).ToList();



            return Json(result);
        }
    }
}
