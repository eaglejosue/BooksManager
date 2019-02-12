using System.Collections.Generic;
using System.Threading.Tasks;
using BooksManager.Application.Interfaces;
using BooksManager.Application.ViewModels;
using DotNetCore.Objects;
using System;
using AutoMapper;
using BooksManager.Domain.Interfaces;
using BooksManager.Domain.Entities;
using AutoMapper.QueryableExtensions;

namespace BooksManager.Application.Services
{
    public class BookingAppService : IBookingAppService
    {
        private readonly IMapper _mapper;
        private readonly IBookingService _bookingService;

        public BookingAppService(
            IMapper mapper,
            IBookingService bookingService)
        {
            _mapper = mapper;
            _bookingService = bookingService;
        }

        public async Task<IResult<IEnumerable<BookingViewModel>>> GetAllAsync()
        {
            var allBookingsResult = await _bookingService.GetAllAsync();
            var allBookingdViewModels = allBookingsResult.Data.ProjectTo<BookingViewModel>();
            return new SuccessResult<IEnumerable<BookingViewModel>>(allBookingdViewModels);
        }

        public async Task<IResult<BookingViewModel>> GetByIdAsync(long id)
        {
            var bookingResult = await _bookingService.GetByIdAsync(id);
            var bookingViewModel = _mapper.Map<BookingViewModel>(bookingResult.Data);
            return new SuccessResult<BookingViewModel>(bookingViewModel);
        }

        public async Task<IResult<BookingViewModel>> AddAsync(BookingViewModel bookingViewModel)
        {
            var booking = _mapper.Map<Booking>(bookingViewModel);
            var bookingResult = await _bookingService.AddAsync(booking);
            bookingViewModel = _mapper.Map<BookingViewModel>(bookingResult.Data);
            return new SuccessResult<BookingViewModel>(bookingViewModel);
        }

        public async Task<IResult<BookingViewModel>> UpdateAsync(BookingViewModel bookingViewModel)
        {
            var booking = _mapper.Map<Booking>(bookingViewModel);
            var bookingResult = await _bookingService.UpdateAsync(booking);
            bookingViewModel = _mapper.Map<BookingViewModel>(bookingResult.Data);
            return new SuccessResult<BookingViewModel>(bookingViewModel);
        }

        public Task<IResult<long>> RemoveAsync(long id)
        {
            return _bookingService.RemoveAsync(id);
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
