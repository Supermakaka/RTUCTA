using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Services;
using PagedList;
using PagedList.Mvc;

using AutoMapper;

using BusinessLogic.Models;

namespace WebSite.Controllers
{
    using Core;
    using ViewModels.Admin;
    using ViewModels.Orders;
    using ViewModels.Shared;
    using System.Net;

    [Authorize(Roles="Admin")]
    public class AdminController : BaseController
    {
        private const int PageSize = 10;
        
        private IUserService userService;
        private ICompanyService companyService;
        private IUserOrderService orderService;

        public AdminController(IUserService userService, ICompanyService companyService, IUserOrderService orderService)
        {
            this.userService = userService;
            this.companyService = companyService;
            this.orderService = orderService;
        }

        public ActionResult UserList(int? page)
        {
            var pageNumber = page ?? 1;

            var users = userService.GetAllUsersWithCompanies();

            var usersViewList = Mapper.Map<IEnumerable<User>, IEnumerable<UserListViewModel>>(users);

            var model = usersViewList.ToPagedList(pageNumber, PageSize);

            return View(model);
        }

        public ActionResult OrderList(int? page)
        {
            var pageNumber = page ?? 1;

            var orders = orderService.GetAllOrdersWithUsers();

            var ordersViewList = Mapper.Map<IEnumerable<UserOrder>, IEnumerable<UserOrderListViewModel>>(orders);

            var model = ordersViewList.ToPagedList(pageNumber, PageSize);

            return View(model);
        }

        public ActionResult CompanyList(int? page)
        {
            var pageNumber = page ?? 1;

            var company = companyService.GetAllCompany();

            var companyViewList = Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyListViewModel>>(company);

            var model = companyViewList.ToPagedList(pageNumber, PageSize);

            return View(model);
        }

        [HttpPost]
        [AutoMap(typeof(User), typeof(UserListViewModel))]
        public ActionResult DisableOrEnableUser(int id)
        {
            var user = userService.GetById(id);

            userService.DisableOrEnable(user);

            return PartialView("UserListRow", user);
        }

        public ActionResult EditUser(int? id)
        {
            var user = (User)userService.GetById(id.HasValue ? id.Value : HttpContextStorage.CurrentUser.Id);

            var companies = companyService.GetAll().ToList();

            var editUserViewModel = new EditUserViewModel(companies);
            editUserViewModel = Mapper.Map<User, EditUserViewModel>(user, editUserViewModel);

            return View(editUserViewModel);
        }

        [HttpPost]
        public ActionResult EditUser(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userToEdit = (User)userService.GetById(model.Id);
            var company = companyService.GetById(model.CompanyId);

            userToEdit.Company = company;
            userToEdit.FirstName = model.FirstName;
            userToEdit.LastName = model.LastName;
            userToEdit.Email = model.Email;
            userToEdit.Disabled = model.Disabled;

            if (!String.IsNullOrEmpty(model.NewPassword))
                userToEdit.Password = userService.HashPassword(model.NewPassword);

            userService.Update(userToEdit);

            return RedirectToAction("UserList");
        }

        public ActionResult EditOrder(int? id)
        {
            var order = orderService.GetById(id.Value);
            
            var users = userService.GetAllUsers().ToList();

            var editUserOrderViewModel = new CreateEditUserOrderViewModel(users);
            editUserOrderViewModel = Mapper.Map<UserOrder, CreateEditUserOrderViewModel>(order, editUserOrderViewModel);

            return View(editUserOrderViewModel);
        }

        [HttpPost]
        public ActionResult EditOrder(CreateEditUserOrderViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var orderToEdit = orderService.GetById(model.ID);
            var user = (User)userService.GetById(model.UserId);

            orderToEdit.User = user;
            orderToEdit.Name = model.Name;
            orderToEdit.Price = model.Price;
            orderToEdit.Description = model.Description;

            orderService.Update(orderToEdit);

            return RedirectToAction("OrderList");
        }

        public ActionResult EditCompany(int? id)
        {
            var company = companyService.GetById(id.Value);

            var editCompanyViewModel = new CreateEditCompanyViewModel();
            editCompanyViewModel = Mapper.Map<Company, CreateEditCompanyViewModel>(company, editCompanyViewModel);

            return PartialView(editCompanyViewModel);
        }

        [HttpPost]
        public ActionResult EditCompany(CreateEditCompanyViewModel model)
        {
            if (!ModelState.IsValid)
                return PartialView(model);
            
            var companyToEdit = companyService.GetById(model.ID);

            companyToEdit.Name = model.Name;

            companyService.Update(companyToEdit);

            return Json(new { success = true });
        }

        public ActionResult CreateOrder()
        {
            var users = userService.GetAllUsers().ToList();
            var setUserOrderViewModel = new CreateEditUserOrderViewModel(users);

            return View(setUserOrderViewModel);
        }

        [HttpPost]
        public ActionResult CreateOrder(CreateEditUserOrderViewModel model)
        {
            if (!ModelState.IsValid)
                return PartialView(model);

            var newOrder = new UserOrder();

            newOrder.UserId = model.UserId;
            newOrder.Name = model.Name;
            newOrder.Price = model.Price;
            newOrder.Description = model.Description;

            orderService.Add(newOrder);

            return RedirectToAction("OrderList");
        }

        public ActionResult CreateCompany()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult CreateCompany(CreateEditCompanyViewModel model)
        {
            if (!ModelState.IsValid)
                return PartialView();

            var newCompany = new Company();

            newCompany.Name = model.Name;

            companyService.Add(newCompany);

            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult DeleteOrder(int? id)
        {
            var order = orderService.GetById(id.Value);
            
            orderService.Delete(order);

            return RedirectToAction("OrderList");
        }

        [HttpGet]
        public ActionResult DeleteCompany(int? id)
        {
            var company = companyService.GetById(id.Value);

            var deleteCompanyViewModel = new CreateEditCompanyViewModel();
            deleteCompanyViewModel = Mapper.Map<Company, CreateEditCompanyViewModel>(company, deleteCompanyViewModel);

            return PartialView(deleteCompanyViewModel);
        }

        [HttpPost]
        public ActionResult DeleteCompanyById(int? id)
        {
            var company = companyService.GetById(id.Value);

            if (!company.Users.Any(u => u.Company == company))
            {
                companyService.Delete(company);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alertmessage = "Cannot delete company because company has users." });
            }
        }
    }
}