using BooksManager.Domain.Entities;
using BooksManager.Domain.Interfaces.Repository;
using BooksManager.Infra.Data.Context;

namespace BooksManager.Infra.Data.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(BooksManagerContext context) : base(context) { }
    }
}
