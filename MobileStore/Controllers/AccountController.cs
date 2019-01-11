using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MobileStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MobileStore.Controllers
{
    public class AccountController : Controller
    {
        ApplicationContext context = new ApplicationContext();
        private IdentityRole adminRole = new IdentityRole { Name = "admin" };
        private IdentityRole userRole = new IdentityRole { Name = "user" };
        
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);


                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                                       DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("MainView", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                var roleManager = new RoleManager<IdentityRole>(new
                    RoleStore<IdentityRole>(context));
                roleManager.Create(adminRole);
                roleManager.Create(userRole);
                if (result.Succeeded)
                {
                    if (user.Email == "Kilar07@mail.ru")
                    {
                        UserManager.AddToRole(user.Id, adminRole.Name);
                        UserManager.AddToRole(user.Id, userRole.Name);
                    }
                    else
                    {
                        UserManager.AddToRole(user.Id, userRole.Name);
                    }
                    return RedirectToAction("MainView", "Home");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }
    }
}