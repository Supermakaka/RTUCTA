using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.ViewModels.Admin;

using AutoMapper;
using BusinessLogic.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace WebSite.ViewModels.Shared
{
    public class CreateEditCompanyViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}