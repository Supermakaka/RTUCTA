using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using AutoMapper;

using BusinessLogic.Models;
using BusinessLogic.Services;

using BusinessLogic.Models.ValidationAttributes;

namespace WebSite.ViewModels.Auth
{
    using ViewModels.Shared;

    public class RegisterViewModel
    {
        public IList<CompanyListViewModel> Companies { get; set; }

        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [UniqueEmail]
        [Required]
        public virtual string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    
        public RegisterViewModel()
        { }

        public RegisterViewModel(IList<Company> companies)
        {
            Companies = Mapper.Map<IList<Company>, IList<CompanyListViewModel>>(companies);
        }
    }
}