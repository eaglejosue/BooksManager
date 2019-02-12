using BooksManager.Domain.Entities;
using BooksManager.Domain.Interfaces.Repository;
using BooksManager.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BooksManager.Infra.Data.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BooksManagerContext context) : base(context) {}

        public Customer GetByEmail(string email)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Email == email);
        }
    }
}
