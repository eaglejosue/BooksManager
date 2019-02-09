using BooksManager.Domain.Models;
using BooksManager.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace BooksManager.Domain.Entities
{
    public class Book : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public ICollection<Booking> Bookings { get; private set; }

        public Book(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        // EF Blank Constructor
        protected Book() { }

        public bool IsAvailable(BookingPeriod requestedBookingPeriod)
        {
            return !Bookings.Any(b => b.BookingPeriod.OverlappedBy(requestedBookingPeriod));
        }
    }
}
