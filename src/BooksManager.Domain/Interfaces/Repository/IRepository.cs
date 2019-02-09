using BooksManager.Domain.Models;
using System;
using System.Linq;

namespace BooksManager.Domain.Interfaces.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        TEntity Add(TEntity obj);
        TEntity GetById(long id);
        IQueryable<TEntity> GetAll();
        TEntity Update(TEntity obj);
        void Remove(long id);
        int SaveChanges();
    }
}
