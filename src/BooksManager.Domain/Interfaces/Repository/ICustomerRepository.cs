using BooksManager.Domain.Entities;

namespace BooksManager.Domain.Interfaces.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetByEmail(string email);
    }
}
