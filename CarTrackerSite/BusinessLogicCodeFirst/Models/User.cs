using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace BusinessLogic.Models
{
    public class User : BaseUser
    {
        public virtual Company Company { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public UserRoles Role { get; set; }

        public virtual ICollection<UserOrder> Orders { get; set; }
    }
}
