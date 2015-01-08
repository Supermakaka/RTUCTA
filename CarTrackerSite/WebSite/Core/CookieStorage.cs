using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogic.Models;
using System.Web.Mvc;
using BusinessLogic.Services;

namespace WebSite.Core
{
    public class CookieStorage
    {
        private static readonly string COOKIE_NAME = "car-id";

        public static int? CurrentCarId
        {
            get
            {
                if(!IsCurrentCarSaved())
                {
                    return null;
                }

                return int.Parse(HttpContext.Current.Request.Cookies[COOKIE_NAME].Value);
            }
            
            set 
            {
                if (HttpContext.Current.Request.Cookies.AllKeys.Contains(COOKIE_NAME))
                {
                    var cookie = HttpContext.Current.Request.Cookies[COOKIE_NAME];

                    if (value == null)
                    {
                        cookie.Expires = DateTime.Now.AddDays(-1);
                    }
                    else
                    {
                        cookie.Value = value.ToString();
                    }

                    HttpContext.Current.Response.SetCookie(cookie);
                }
                else
                {
                    if (value != null)
                    {
                        var cookie = new HttpCookie(COOKIE_NAME);

                        cookie.Value = value.ToString();
                        HttpContext.Current.Response.Cookies.Add(cookie);
                    }
                }
            }
        }

        public static bool IsCurrentCarSaved()
        {
            return HttpContext.Current.Request.Cookies.AllKeys.Contains(COOKIE_NAME);
        }

        public static void RemoveCurrentCar() 
        { 
            if(HttpContext.Current.Request.Cookies.AllKeys.Contains(COOKIE_NAME))
            {
                var cookie = HttpContext.Current.Request.Cookies[COOKIE_NAME];

                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.SetCookie(cookie);
            }
        }
    }
}