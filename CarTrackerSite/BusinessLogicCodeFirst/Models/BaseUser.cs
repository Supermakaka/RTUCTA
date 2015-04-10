using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

using BusinessLogic.Services;
using BusinessLogic.Models.ValidationAttributes;

namespace BusinessLogic.Models
{
    [Table("User")]
    public abstract class BaseUser
    {
        public BaseUser()
        {
            Disabled = false;
        }

        public int Id { get; set; }

        [UniqueEmail]
        [Required]
        public virtual string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool Disabled { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
