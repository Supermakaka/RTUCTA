using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.ViewModels.Admin;

using AutoMapper;
using BusinessLogic.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace WebSite.ViewModels.Orders
{
    public class CreateEditUserOrderViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public IList<UserListViewModel> Users { get; set; }

        public CreateEditUserOrderViewModel()
        { }

        public CreateEditUserOrderViewModel(IList<User> users)
        {
            Users = Mapper.Map<IList<User>, IList<UserListViewModel>>(users);
        }
    }
}