using BooksManager.Domain.Interfaces;
using System;

namespace BooksManager.Domain.Services
{
    public class BookingService : IBookingService
    {


        public void Dispose() => GC.SuppressFinalize(this);
    }
}
