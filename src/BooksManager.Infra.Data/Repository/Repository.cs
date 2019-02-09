using BooksManager.Domain.Interfaces.Repository;
using BooksManager.Domain.Models;
using BooksManager.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BooksManager.Infra.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly BooksManagerContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(BooksManagerContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual TEntity Add(TEntity obj)
        {
            return DbSet.Add(obj).Entity;
        }

        public virtual TEntity GetById(long id)
        {
            return DbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public virtual TEntity Update(TEntity obj)
        {
            return DbSet.Update(obj).Entity;
        }

        public virtual void Remove(long id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
