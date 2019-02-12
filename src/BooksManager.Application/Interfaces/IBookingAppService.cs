using BooksManager.Application.ViewModels;
using DotNetCore.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksManager.Application.Interfaces
{
    public interface IBookingAppService : IDisposable
    {
        Task<IResult<IEnumerable<BookingViewModel>>> GetAllAsync();
        Task<IResult<BookingViewModel>> GetByIdAsync(long id);
        Task<IResult<BookingViewModel>> AddAsync(BookingViewModel bookingViewModel);
        Task<IResult<BookingViewModel>> UpdateAsync(BookingViewModel bookingViewModel);
        Task<IResult<long>> RemoveAsync(long id);

        IResult<IEnumerable<BookingViewModel>> GetAll();
        IResult<BookingViewModel> GetById(long id);
        IResult<BookingViewModel> Add(BookingViewModel bookingViewModel);
        IResult<BookingViewModel> Update(BookingViewModel bookingViewModel);
        IResult<long> Remove(long id);
    }
}
