using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using BusinessLogic.Services;
using BusinessLogic.Models;
using System.Collections.Generic;
using WebSite.Controllers;
using System.Web.Mvc;
using WebSite.ViewModels.Admin;


namespace Tests.WebSite.Tests.Controllers
{
    [TestClass]
    public class AdminControllerTests : BaseWebSiteTests
    {
        public IUserService userService;
        public ICompanyService companyService;
        public IUserOrderService orderService;

        public User CreateUserWithCompany()
        {
            var company = new Company()
            {
                Id = 1,
                Name = "testCompany1"
            };

            var user = new User()
            {
                Id = 1,
                CreateDate = DateTime.Now,
                Disabled = false,
                Email = "testUser@example.com",
                FirstName = "testUser",
                LastName = "testUser",
                Password = userService.HashPassword("12345"),
                Role = UserRoles.Member,
                Company = company
            };

            return user;
        }

        public List<Company> CreateCompanies()
        {
            return new List<Company>
            {
                new Company()
                {
                    Id = 1,
                    Name = "TestCompany1"
                },
                new Company()
                {
                    Id = 2,
                    Name = "TestCompant2"
                }
            };
        }

        [TestInitialize]
        public void TestInitialize()
        {
            base.TestInitialize();

            userService = MockRepository.GenerateStub<IUserService>();
            companyService = MockRepository.GenerateStub<ICompanyService>();         
        }

        [TestMethod]
        public void Test_UserList_View()
        {
            var user = CreateUserWithCompany();
            userService.Stub(x => x.GetAllUsersWithCompanies()).Return(new List<User> { user });
  
            var adminController = new AdminController(userService, companyService, orderService);
            
            var viewResult = adminController.UserList(null) as ViewResult;
            var dataModel = ((IEnumerable<UserListViewModel>)viewResult.ViewData.Model).ToList();
           
            Assert.AreEqual(dataModel.Count, 1);
            Assert.IsTrue(dataModel.Any(d=>d.Email.Contains("testUser@example.com")));
        }

        [TestMethod]
        public void Test_DisableOrEnableUser_View()
        {
            var user = CreateUserWithCompany();
            
            userService.Stub(x => x.GetById(1)).Return(user);
            userService.Expect(x => x.DisableOrEnable(user)).WhenCalled(c=> user.Disabled = true);

            var adminController = new AdminController(userService, companyService, orderService);
            var viewResult = adminController.DisableOrEnableUser(user.Id) as PartialViewResult;
            var dataModel = (User)viewResult.ViewData.Model;

            Assert.AreEqual("UserListRow", viewResult.ViewName);
            Assert.IsTrue(dataModel.Disabled);
        }

        [TestMethod]
        public void Test_EditUser_View()
        {
            var user = CreateUserWithCompany();
            var companies = CreateCompanies();

            userService.Stub(x => x.GetById(1)).Return(user);
            companyService.Stub(x => x.GetAll()).Return(companies);

            var adminController = new AdminController(userService, companyService, orderService);
            var viewResult = adminController.EditUser(user.Id) as ViewResult;
            var dataModel = (EditUserViewModel)viewResult.ViewData.Model;


            Assert.AreEqual("testUser@example.com", dataModel.Email);
            Assert.AreEqual(2, dataModel.Companies.Count);
        }

        [TestMethod]
        public void Test_EditUser_View_Post()
        {
            var updatedUser = new User();
            var user = CreateUserWithCompany();

            userService.Stub(x => x.GetById(1)).Return(user);
            userService.Expect(x => x.Update(user)).WhenCalled(c => updatedUser = user);

            companyService.Stub(x => x.GetById(2)).Return(new Company() { Id = 2, Name = "TestCompany2" });

            var editUserViewModel = new EditUserViewModel()
            {
                Id = 1,
                CompanyId = 2,
                Disabled = true,
                Email = "testUser2@example.com",
                FirstName = "testUser2",
                LastName = "testUser2"
            };


            var adminController = new AdminController(userService, companyService, orderService);


            var result = (RedirectToRouteResult)adminController.EditUser(editUserViewModel);

            Assert.AreEqual(updatedUser.Email, "testUser2@example.com");
            Assert.AreEqual(updatedUser.FirstName, "testUser2");
            Assert.AreEqual(updatedUser.LastName, "testUser2");
            Assert.AreEqual(updatedUser.Company.Name, "TestCompany2");
            Assert.IsTrue(updatedUser.Disabled);
            Assert.AreEqual("UserList", result.RouteValues["Action"]);
            
        }
    }
}
