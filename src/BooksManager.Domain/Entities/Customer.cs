using BooksManager.Domain.Models;
using System;
using System.Collections.Generic;

namespace BooksManager.Domain.Entities
{
    public class Customer : Entity
    {
        public Customer(long id, string name, string email, string telephone, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            Telephone = telephone;
            BirthDate = birthDate;
        }

        // Empty constructor for EF
        protected Customer() { }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Telephone { get; private set; }

        public DateTime BirthDate { get; private set; }

        // EF Relations
        public ICollection<Booking> Bookings { get; set; }
    }
}
