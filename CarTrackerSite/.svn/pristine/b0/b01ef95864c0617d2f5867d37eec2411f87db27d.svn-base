using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogic.Models;
using System.Web.Mvc;
using BusinessLogic.Services;

namespace WebSite.Core
{
    public class HttpContextStorage
    {
        public static BaseUser CurrentUser
        {
            get
            {
                var currentContext = HttpContext.Current;
 
                if (currentContext.Items["CURRENT_USER"] == null)
                {
                    var identity = currentContext.User.Identity; 

                    if (identity != null && identity.IsAuthenticated)
                    {
                        return CurrentUser = DependencyResolver.Current.GetService<IUserService>().Get(u => u.Email == identity.Name);
                    }

                    return null;
                }
                return (BaseUser)currentContext.Items["CURRENT_USER"];

            }
            set
            {
                HttpContext.Current.Items["CURRENT_USER"] = value;
            }
        }
    }
}