using BooksManager.Domain.Entities;
using DotNetCore.Objects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BooksManager.Domain.Interfaces
{
    public interface IBookingService : IDisposable
    {
        Task<IResult<IQueryable<Booking>>> GetAllAsync();
        Task<IResult<Booking>> GetByIdAsync(long id);
        Task<IResult<Booking>> AddAsync(Booking booking);
        Task<IResult<Booking>> UpdateAsync(Booking booking);
        Task<IResult<long>> RemoveAsync(long id);
    }
}
