using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SignalRProjectUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace SignalRProjectUI.Controllers
{
    public class IdentityController : Controller
    {
        Context db = new Context();

        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            if (returnUrl==null)
            {
                returnUrl = "/Home";
            }
            ViewData["ReturnUrl"] = returnUrl;
           
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(User user,string returnUrl)
        {
           var userValidate =  db.users.FirstOrDefault(x => x.userName == user.userName && x.password==user.password);
          
            if(userValidate!=null)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(user.userName, user.userName));
                claims.Add(new Claim(ClaimTypes.Name, user.userName));
                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                
               

                return Redirect(returnUrl);
            }
            return View();
        }
      

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult Register()
        {



            return View();
        }

        public IActionResult Register (SignalRProjectUI.Models.User data)
        {
            if (data!=null)
            {
                User user = new User()
                {
                    userName = data.userName,
                    password = data.password,
                    DateOfBirth = data.DateOfBirth,
                    Email = data.Email,
                    EmailConfirm = 0,
                    Gender = data.Gender,
                    Name = data.Name,
                    SurName = data.SurName


                };

                db.users.Add(user);
                db.SaveChanges();

            }
        


            return RedirectToAction("Index","Chat");
        }

    }
}
