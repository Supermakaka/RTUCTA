using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Core.NinjectModules;

namespace Tests.WebSite.Tests
{
    [TestClass]
    public class BaseWebSiteTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            var kernel = new StandardKernel(new AutoMapperModule());
        }
    }
}
