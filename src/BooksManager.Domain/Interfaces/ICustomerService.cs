using BooksManager.Domain.Entities;
using DotNetCore.Objects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BooksManager.Domain.Interfaces
{
    public interface ICustomerService : IDisposable
    {
        Task<IResult<IQueryable<Customer>>> GetAllAsync();
        Task<IResult<Customer>> GetByIdAsync(long id);
        Task<IResult<Customer>> AddAsync(Customer customer);
        Task<IResult<Customer>> UpdateAsync(Customer customer);
        Task<IResult<long>> RemoveAsync(long id);
    }
}
