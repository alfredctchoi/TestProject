using System;
using System.Collections.Generic;
using TestProject.Model.Domain;
using TestProject.Model.Enums;
using TestProject.Service;
using UserRole = TestProject.Model.Domain.UserRole;

namespace TestProject.Database.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Context.TestContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TestProject.Database.Context.TestContext";
        }

        protected override void Seed(Context.TestContext context)
        {

            var user = new User
            {
                Password = PasswordUtility.Encrypt("Alfred"),
                Created = DateTime.UtcNow,
                Deleted = false,
                Email = "alfred.ct.choi@gmail.com",
                FirstName = "Alfred",
                LastName = "Choi",
                StatusEnum = UserStatusEnum.Active,
                Roles = new List<UserRole>
                {
                    new UserRole
                    {
                        Role = UserRoleEnum.Admin
                    }
                }
            };

            context.Users.AddOrUpdate(user);

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
