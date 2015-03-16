using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
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
        public DbSet<Country>Countries { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            buildUser(modelBuilder);
            buildVendor(modelBuilder);
            buildBank(modelBuilder);
            buildCountryAndState(modelBuilder);
        }

        private void buildCountryAndState(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .HasKey(c => c.CountryId);

            modelBuilder.Entity<Country>()
                .Property(c => c.IsoLong)
                .HasColumnType("VARCHAR")
                .HasMaxLength(10)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("XI_IsoLong") {IsUnique = true}));

            modelBuilder.Entity<Country>()
                .Property(c => c.IsoShort)
                .HasColumnType("VARCHAR")
                .HasMaxLength(10)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("XI_IsoShort") { IsUnique = true }));
            
            modelBuilder.Entity<Country>()
                .HasOptional(c => c.CreatedUser)
                .WithMany()
                .HasForeignKey(c => c.CreatedUserId);
            
            modelBuilder.Entity<Country>()
                .HasOptional(c => c.ModifiedUser)
                .WithMany()
                .HasForeignKey(c => c.ModifiedUserId);

            modelBuilder.Entity<State>()
                .HasKey(c => c.StateId);

            modelBuilder.Entity<State>()
                .HasOptional(c => c.CreatedUser)
                .WithMany()
                .HasForeignKey(c => c.CreatedUserId);

            modelBuilder.Entity<State>()
                .HasOptional(c => c.ModifiedUser)
                .WithMany()
                .HasForeignKey(c => c.ModifiedUserId);

            modelBuilder.Entity<State>()
                .HasRequired(s => s.Country)
                .WithMany()
                .HasForeignKey(s => s.CountryId);
        }

        private void buildBank(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bank>()
               .HasKey(b => b.VendorId);

            modelBuilder.Entity<Bank>()
                .HasOptional(b => b.State)
                .WithMany()
                .HasForeignKey(b => b.StateId);
        }

        private void buildUser(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("XI_UserEmail") {IsUnique = true}));

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
