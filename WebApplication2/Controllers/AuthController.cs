using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication2.Models;
using System.Linq;

namespace WebApplication2.Controllers
{
    public class AuthController : Controller
    {
        WEBINFOEntities entity = new WEBINFOEntities();
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel credentials)
        {
            bool isExist = entity.Users.Any(x => x.email == credentials.Email && x.password == credentials.Password) ;
            User u = entity.Users.FirstOrDefault(x => x.email == credentials.Email && x.password == credentials.Password);
            
            if(isExist)
            {
                FormsAuthentication.SetAuthCookie(u.name, false);
                return RedirectToAction("index", "Employees");
            }

            ModelState.AddModelError("", "Username or Password is wrong");
            return View();
        }
        [HttpPost]
        public ActionResult Signup(User userinfo)
        {
            entity.Users.Add(userinfo);
            entity.SaveChanges();
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}