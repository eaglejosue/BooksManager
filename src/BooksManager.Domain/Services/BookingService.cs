using BooksManager.Domain.Entities;
using BooksManager.Domain.Exception;
using BooksManager.Domain.Interfaces;
using BooksManager.Domain.Interfaces.Repository;
using DotNetCore.Objects;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BooksManager.Domain.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(
            IBookingRepository bookingRepository,
            IUnitOfWork unitOfWork)
        {
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<IResult<IQueryable<Booking>>> GetAllAsync()
        {
            var allBookings = _bookingRepository.GetAll();
            return new SuccessResult<IQueryable<Booking>>(allBookings).ToTask();
        }

        public Task<IResult<Booking>> GetByIdAsync(long id)
        {
            var booking = _bookingRepository.GetById(id);
            return new SuccessResult<Booking>(booking).ToTask();
        }

        public Task<IResult<Booking>> AddAsync(Booking booking)
        {
            var bookValidationResult = new AddNewBookingValidation().Validate(booking);
            if (!bookValidationResult.IsValid)
                return new ErrorResult<Booking>(bookValidationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty).ToTask();

            var bookingEntity = _bookingRepository.Add(booking);
            if (!_unitOfWork.Commit()) throw new ExceptionHandler(HttpStatusCode.BadRequest, "A problem occurred during saving the data.");

            return new SuccessResult<Booking>(bookingEntity).ToTask();
        }

        public Task<IResult<Booking>> UpdateAsync(Booking booking)
        {
            var bookValidationResult = new UpdateBookingValidation().Validate(booking);
            if (!bookValidationResult.IsValid)
                return new ErrorResult<Book>(bookValidationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty).ToTask();

            if (_bookingRepository.GetById(booking.Id) == null)
                throw new ExceptionHandler(HttpStatusCode.NotFound, $"Book id {booking.Id} not found.");

            booking = _bookingRepository.Update(booking);

            if (!_unitOfWork.Commit()) throw new ExceptionHandler(HttpStatusCode.BadRequest, "A problem occurred during saving the data.");

            return new SuccessResult<Booking>(booking).ToTask();
        }

        public Task<IResult<long>> RemoveAsync(long id)
        {
            var book = _bookingRepository.GetById(id);
            if (book == null) throw new ExceptionHandler(HttpStatusCode.NotFound, $"Book id {book.Id} not found.");

            var bookValidationResult = new RemoveBookingValidation().Validate(book);
            if (!bookValidationResult.IsValid)
                return new ErrorResult<long>(bookValidationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty).ToTask();

            _bookingRepository.Remove(id);

            if (!_unitOfWork.Commit()) throw new ExceptionHandler(HttpStatusCode.BadRequest, "A problem occurred during saving the data.");

            return new SuccessResult<long>(id).ToTask();
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
