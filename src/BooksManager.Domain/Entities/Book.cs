using BooksManager.Domain.Models;
using BooksManager.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace BooksManager.Domain.Entities
{
    public class Book : Entity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public string Author { get; private set; }
        public int Year { get; private set; }
        public string Publisher { get; private set; }
        public int Edition { get; private set; }
        public string Tag { get; private set; }
        public string Summary { get; private set; }
        //public byte[] Image { get; private set; }

        public ICollection<Booking> Bookings { get; private set; }

        public Book(long id, string title, string description, decimal price, string author, int year, 
            string publisher, int edition, string tag, string summary/*, byte[] image*/)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            Author = author;
            Year = year;
            Publisher = publisher;
            Edition = edition;
            Tag = tag;
            Summary = summary;
            //Image = Image;
        }

        // EF Blank Constructor
        protected Book() { }

        public bool IsAvailable(BookingPeriod requestedBookingPeriod)
        {
            return !Bookings.Any(b => b.BookingPeriod.OverlappedBy(requestedBookingPeriod));
        }
    }
}
