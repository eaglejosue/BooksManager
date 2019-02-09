using BooksManager.Domain.Interfaces;
using BooksManager.Infra.Data.Context;

namespace BooksManager.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BooksManagerContext _context;

        public UnitOfWork(BooksManagerContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
