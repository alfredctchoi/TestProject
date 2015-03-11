using System.Data.Entity;
using TestProject.Database.Context.Interface;
using TestProject.Model.Domain;

namespace TestProject.Database.Context
{
    public class TestContext: DbContext, ITestContext
    {
        public TestContext()
            :base("name=TestProject")
        {
            System.Data.Entity.Database.SetInitializer(new CreateDatabaseIfNotExists<TestContext>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Bank> Banks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            buildUser(modelBuilder);
            buildVendor(modelBuilder);
            buildBank(modelBuilder);
        }

        private void buildBank(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bank>()
               .HasKey(b => b.VendorId);
        }

        private void buildUser(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOptional(u => u.CreatedUser)
                .WithMany()
                .HasForeignKey(u => u.CreatedUserId);

            modelBuilder.Entity<User>()
                .HasOptional(u => u.ModifiedUser)
                .WithMany()
                .HasForeignKey(u => u.ModifiedUserId);

            modelBuilder.Entity<UserRole>()
               .HasKey(ur => ur.UserRoleId)
               .HasOptional(ur => ur.User)
               .WithMany(u => u.Roles)
               .HasForeignKey(r => r.UserId);
        }

        private void buildVendor(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vendor>()
                .HasOptional(u => u.CreatedUser)
                .WithMany()
                .HasForeignKey(u => u.CreatedUserId);

            modelBuilder.Entity<Vendor>()
                .HasOptional(u => u.ModifiedUser)
                .WithMany()
                .HasForeignKey(u => u.ModifiedUserId);

            modelBuilder.Entity<Vendor>()
                .HasOptional(v => v.Bank)
                .WithRequired(b => b.Vendor);
        }
    }
}
