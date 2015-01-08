using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Services;

namespace WebSite.Controllers
{
    [Authorize(Roles="Member,Manager,Supervisor")]
    public class UserController : BaseController
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult Account()
        {
            return View();
        }
    }
}
