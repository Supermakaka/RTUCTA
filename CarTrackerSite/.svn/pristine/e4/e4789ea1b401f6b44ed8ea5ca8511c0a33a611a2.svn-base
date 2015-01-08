using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebSite.ViewModels.Auth;
using BusinessLogic.Services;
using BusinessLogic.Models;
using WebSite.Core;

namespace WebSite.Controllers
{
    public class AuthController : BaseController
    {
        private IUserService userService;
        private ICompanyService companyService;

        public AuthController(IUserService userService, ICompanyService companyService)
        {
            this.userService = userService;
            this.companyService = companyService;
        }

        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult LogInVerticalForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LogInViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    var currentUser = userService.Get(u => u.Email == model.UserName);
                    
                    FormsAuthentication.SetAuthCookie(currentUser.Email, model.RememberMe);
                    
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        if (currentUser is Admin)
                        {
                            return RedirectToAction("UserList", "Admin");
                        }

                        return RedirectToAction("Account", "User"); 
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            var companies = companyService.GetAll().ToList();

            var model = new RegisterViewModel(companies);

            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var company = companyService.GetById(model.CompanyId);

            var user = new User();
            
            user.Company = company;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Password = userService.HashPassword(model.ConfirmPassword);
            user.Role = UserRoles.Member;

            userService.Add(user);                

            FormsAuthentication.SetAuthCookie(model.Email, false);
                
            return RedirectToAction("Index", "Home");
        }       
    }
}
