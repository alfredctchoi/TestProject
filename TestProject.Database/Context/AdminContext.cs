using System.Data.Entity;
using TestProject.Model.Domain;

namespace TestProject.Database.Context
{
    public class AdminContext: DbContext
    {
        public AdminContext()
            :base("name=TestProject")
        {
            System.Data.Entity.Database.SetInitializer(new CreateDatabaseIfNotExists<AdminContext>());
        }

        public DbSet<User> Users { get; set; }

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
