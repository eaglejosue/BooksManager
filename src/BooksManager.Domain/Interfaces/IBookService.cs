using BooksManager.Domain.Entities;
using DotNetCore.Objects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BooksManager.Domain.Interfaces
{
    public interface IBookService : IDisposable
    {
        Task<IResult<IQueryable<Book>>> GetAllAsync();
        Task<IResult<Book>> GetByIdAsync(long id);
        Task<IResult<Book>> AddAsync(Book book);
        Task<IResult<Book>> UpdateAsync(Book book);
        Task<IResult<long>> RemoveAsync(long id);

        IResult<IQueryable<Book>> GetAll();
        IResult<Book> GetById(long id);
        IResult<Book> Add(Book book);
        IResult<Book> Update(Book book);
        IResult<long> Remove(long id);
    }
}
