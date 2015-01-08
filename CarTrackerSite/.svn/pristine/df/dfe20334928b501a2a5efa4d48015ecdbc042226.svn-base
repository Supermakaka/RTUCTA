using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Web.Mvc;
using BusinessLogic.Services;
using BusinessLogic.Models;

namespace WebSite.Core.NinjectModules
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            BusinessLogic.Infrastructure.DependencyResolver.Initialize(Kernel);
 
            //Having single DbContext per request is the best option, that's why InRequestScope()
            //More details: http://stackoverflow.com/questions/10585478/one-dbcontext-per-web-request-why
            Bind<IDataContext>().To<DataContext>().InRequestScope();

            Bind<IUserService>().To<UserService>();
            Bind<ICompanyService>().To<CompanyService>();
            Bind<IUserOrderService>().To<UserOrderService>();
        }
    }
}