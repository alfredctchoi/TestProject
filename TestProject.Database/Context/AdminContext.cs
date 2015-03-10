using System.Data.Entity;
using TestProject.Database.Context.Interface;
using TestProject.Model.Domain;

namespace TestProject.Database.Context
{
    public class AdminContext: DbContext, IAdminContext
    {
        public AdminContext()
            :base("name=TestProject")
        {
            System.Data.Entity.Database.SetInitializer(new CreateDatabaseIfNotExists<AdminContext>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOptional(u => u.CreatedUser)
                .WithMany()
                .HasForeignKey(u => u.CreatedUserId);

            modelBuilder.Entity<User>()
                .HasOptional(u => u.ModifiedUser)
                .WithMany()
                .HasForeignKey(u => u.ModifiedUserId);
        }
    }
}
