using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;

namespace BusinessLogic.Infrastructure
{
    using BusinessLogic.Models;
    using BusinessLogic.Services;

    public class DBInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            List<Company> companies = new List<Company>
            {
                new Company {Id = 1, Name = "Company 1"},
                new Company {Id = 2, Name = "Company 2"}
            };

            // add data into context and save to db
            foreach (Company c in companies)
            {
                context.Companies.Add(c);
            }

            List<BaseUser> users = new List<BaseUser>
            {
                new Admin()
                {
                    Email = "admin@example.com",
                    Password = DependencyResolver.Get<IUserService>().HashPassword("qwerty"),
                    CreateDate = DateTime.Now
                },
                new User()
                {
                    Company = companies[0],
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "john@example.com",
                    Password = DependencyResolver.Get<IUserService>().HashPassword("qwerty"),
                    Role = UserRoles.Member,
                    CreateDate = DateTime.Now
                },
                new User()
                {
                    Company = companies[1],
                    FirstName = "Tom",
                    LastName = "Cruise",
                    Email = "tom@example.com",
                    Password = DependencyResolver.Get<IUserService>().HashPassword("qwerty"),
                    Role = UserRoles.Manager,
                    CreateDate = DateTime.Now
                }
            };

            List<UserOrder> ord = new List<UserOrder>
            {
                new UserOrder()
                {
                    Name = "Chainsaw",
                    Price = 50,
                    Description = "Big, yellow chainsaw",
                    UserId = 2
                },
                new UserOrder()
                {
                    Name = "Hammer",
                    Price = 18,
                    Description = "Thor's hammer",
                    UserId = 2
                }
            };

            // add data into context and save to db
            foreach (BaseUser u in users)
            {
                context.Users.Add(u);
            }

            foreach (UserOrder uo in ord)
            {
                context.UserOrders.Add(uo);
            }

            context.SaveChanges();
        }
    }
}
