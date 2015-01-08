namespace BusinessLogic.Migrations
{
    using BusinessLogic.Infrastructure;
    using BusinessLogic.Models;
    using BusinessLogic.Services;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BusinessLogic.Models.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BusinessLogic.Models.DataContext context)
        {
            List<Company> companies = new List<Company>
            {
                new Company {Id = 1, Name = "Company 1"},
                new Company {Id = 2, Name = "Company 2"}
            };

            context.Users.AddOrUpdate(
                u => u.Id,
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
            );
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
