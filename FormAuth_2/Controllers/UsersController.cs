using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FormAuth_2.Controllers
{
    public class UsersController : Controller
    {
        //[AllowAnonymous]
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }
        // GET: Users

        //[AllowAnonymous]
        [HttpPost]
        public ActionResult login(User model)
        {
            using (MVC_DBEntities context = new MVC_DBEntities())
            {
                var isValid = context.Users.Any(x=>x.UserName == model.UserName && x.UserPassword==x.UserPassword);
                if(isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName,false);
                    return RedirectToAction("Index","Employees");
                }
                ModelState.AddModelError("", "Invalid Cradentials");
                return View();
            }
        }


        //[AllowAnonymous]
        [HttpGet]
        public ActionResult signup()
        {
            return View();
        }
        //[AllowAnonymous]
        [HttpPost]
        public ActionResult signup(User model)
        {
            using (MVC_DBEntities context = new MVC_DBEntities())
            {
                if(ModelState.IsValid)
                {
                    context.Users.Add(model);
                    context.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("","Invalid entry");
                    return View();
                }
               
            }
        }

        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login");
        }
    }
}