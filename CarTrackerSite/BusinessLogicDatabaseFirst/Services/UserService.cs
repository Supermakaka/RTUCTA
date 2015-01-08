using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using BusinessLogic.Models;
using BusinessLogic.Infrastructure;

using BCrypt.Net;

namespace BusinessLogic.Services
{
    public class UserService : BaseService<BaseUser>, IUserService
    {
        public UserService(IDataContext dataContext) 
            : base(dataContext)
        {
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
        }

        public bool IsValid(string email, string password)
        {
            var user = dataContext.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
                return false;

            return BCrypt.Net.BCrypt.Verify(password, user.Password) && !user.Disabled;
        }

        public void DisableOrEnable(BaseUser user)
        {
            user.Disabled = !user.Disabled;

            dataContext.SaveChanges();
        }

        public IEnumerable<User> GetAllUsersWithCompanies()
        {
            return dataContext.Users.OfType<User>()
                .Include(u => u.Company)
                .ToList();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return dataContext.Users.OfType<User>();
        }
    }

    public interface IUserService : IService<BaseUser>
    {
        string HashPassword(string password);
        bool IsValid(string email, string password);
        void DisableOrEnable(BaseUser user);
        IEnumerable<User> GetAllUsersWithCompanies();
        IEnumerable<User> GetAllUsers();
    }
}
