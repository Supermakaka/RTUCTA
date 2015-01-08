using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using AutoMapper;

using BusinessLogic.Models;
using BusinessLogic.Models.ValidationAttributes;

namespace WebSite.ViewModels.Admin
{
    using ViewModels.Shared;

    public class EditUserViewModel
    {
        public int Id { get; set; }

        public IList<CompanyListViewModel> Companies { get; set; }

        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "New Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [UniqueEmail]
        [Required]
        public virtual string Email { get; set; }

        public bool Disabled { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        public EditUserViewModel()
        { }
        
        public EditUserViewModel(IList<Company> companies)
        {
            Companies = Mapper.Map<IList<Company>, IList<CompanyListViewModel>>(companies);
        }
    }
}