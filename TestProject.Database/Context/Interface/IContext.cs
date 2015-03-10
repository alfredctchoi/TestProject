using System;
using System.Data.Entity;

namespace TestProject.Database.Context.Interface
{
    public interface IContext : IDisposable
    {
        int SaveChanges();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
