using BooksManager.Domain.Exception;
using BooksManager.Domain.Models;
using BooksManager.Domain.ValueObjects;
using System;

namespace BooksManager.Domain.Entities
{
    public class Booking : Entity
    {
        public BookingPeriod BookingPeriod { get; private set; }
        public Book Book { get; private set; }
        public decimal Price { get; private set; }
        public long BookId { get; private set; }

        internal Booking(BookingPeriod bookingPeriod, Book book)
        {
            Book = book;
            BookingPeriod = bookingPeriod;
            BookId = book.Id;
            Price = GetFullPrice();
        }

        // EF Blank Constructor
        protected Booking() { }

        // EF Navigation Properties
        public virtual Customer Customer { get; protected set; }

        public static Booking Create(BookingPeriod bookingPeriod, Book book)
        {
            if (!book.IsAvailable(bookingPeriod))
                throw new ExceptionHandler("Livro não disponível.");

            var booking = new Booking(bookingPeriod, book);
            return booking;
        }

        private decimal GetFullPrice()
        {
            return Book.Price;
        }
    }
}
