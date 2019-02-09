using BooksManager.Domain.Entities;
using BooksManager.Domain.Interfaces.Repository;
using BooksManager.Infra.Data.Context;

namespace BooksManager.Infra.Data.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(BooksManagerContext context)
            : base(context) { }
    }
}
