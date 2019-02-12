using BooksManager.Application.ViewModels;
using DotNetCore.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksManager.Application.Interfaces
{
    public interface ICustomerAppService : IDisposable
    {
        Task<IResult<IEnumerable<CustomerViewModel>>> GetAllAsync();
        Task<IResult<CustomerViewModel>> GetByIdAsync(long id);
        Task<IResult<CustomerViewModel>> AddAsync(CustomerViewModel customerViewModel);
        Task<IResult<CustomerViewModel>> UpdateAsync(CustomerViewModel customerViewModel);
        Task<IResult<long>> RemoveAsync(long id);

        IResult<IEnumerable<CustomerViewModel>> GetAll();
        IResult<CustomerViewModel> GetById(long id);
        IResult<CustomerViewModel> Add(CustomerViewModel customerViewModel);
        IResult<CustomerViewModel> Update(CustomerViewModel customerViewModel);
        IResult<long> Remove(long id);
    }
}
