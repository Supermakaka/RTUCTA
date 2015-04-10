using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

using Ninject;

namespace BusinessLogic.Models.ValidationAttributes
{
    using BusinessLogic.Services;
    using BusinessLogic.Infrastructure;

    public class UniqueEmail : ValidationAttribute
    {
        public UniqueEmail()
            : base()
        {
            
        }

        public override string FormatErrorMessage(string name)
        {
            return "This Email already exists.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userService = DependencyResolver.Get<IUserService>();

            var user = userService.Get(u => u.Email == (string)value);

            var currentUserId = 0;

            var idProperty = validationContext.ObjectInstance.GetType().GetProperty("Id");

            if (idProperty != null)
                int.TryParse(idProperty.GetValue(validationContext.ObjectInstance, null).ToString(), out currentUserId);

            if (user != null && user.Id != currentUserId)
            {
                return new ValidationResult(FormatErrorMessage(null));
            }

            return null;
        }
    }
}
