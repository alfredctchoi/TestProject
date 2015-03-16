using System;
using System.Collections.Generic;
using System.Linq;
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

            var countries = new List<Country>
            {
                new Country("Canada", "CA", "CAN"),
                new Country("United States", "US", "USA")
            };

            context.Countries.AddRange(countries);
            context.SaveChanges();
            

            var usaId = context.Countries.First(c => c.IsoShort.Equals("US")).CountryId;
            var states = new List<State>
            {
                new State(usaId, "Alabama", "AL"),
                new State(usaId, "Alaska", "AK"),
                new State(usaId, "Arizona", "AZ"),
                new State(usaId, "Arkansas", "AR"),
                new State(usaId, "California", "CA"),
                new State(usaId, "Colorado", "CO"),
                new State(usaId, "Connecticut", "CT"),
                new State(usaId, "Delaware", "DE"),
                new State(usaId, "District of Columbia", "DC"),
                new State(usaId, "Florida", "FL"),
                new State(usaId, "Georgia", "GA"),
                new State(usaId, "Hawaii", "HI"),
                new State(usaId, "Idaho", "ID"),
                new State(usaId, "Illinois", "IL"),
                new State(usaId, "Indiana", "IN"),
                new State(usaId, "Iowa", "IA"),
                new State(usaId, "Kansas", "KS"),
                new State(usaId, "Kentucky", "KY"),
                new State(usaId, "Louisiana", "LA"),
                new State(usaId, "Maine", "ME"),
                new State(usaId, "Maryland", "MD"),
                new State(usaId, "Massachusetts", "MA"),
                new State(usaId, "Michigan", "MI"),
                new State(usaId, "Minnesota", "MN"),
                new State(usaId, "Mississippi", "MS"),
                new State(usaId, "Missouri", "MO"),
                new State(usaId, "Montana", "MT"),
                new State(usaId, "Nebraska", "NE"),
                new State(usaId, "Nevada", "NV"),
                new State(usaId, "New Hampshire", "NH"),
                new State(usaId, "New Jersey", "NJ"),
                new State(usaId, "New Mexico", "NM"),
                new State(usaId, "New York", "NY"),
                new State(usaId, "North Carolina", "NC"),
                new State(usaId, "North Dakota", "ND"),
                new State(usaId, "Ohio", "OH"),
                new State(usaId, "Oklahoma", "OK"),
                new State(usaId, "Oregon", "OR"),
                new State(usaId, "Pennsylvania", "PA"),
                new State(usaId, "Rhode Island", "RI"),
                new State(usaId, "South Carolina", "SC"),
                new State(usaId, "South Dakota", "SD"),
                new State(usaId, "Tennessee", "TN"),
                new State(usaId, "Texas", "TX"),
                new State(usaId, "Utah", "UT"),
                new State(usaId, "Vermont", "VT"),
                new State(usaId, "Virginia", "VA"),
                new State(usaId, "Washington", "WA"),
                new State(usaId, "West Virginia", "WV"),
                new State(usaId, "Wisconsin", "WI"),
                new State(usaId, "Wyoming", "WY")
            };

            context.States.AddRange(states);
            context.SaveChanges();

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
