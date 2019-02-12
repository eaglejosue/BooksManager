using BooksManager.Application.ViewModels;
using DotNetCore.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksManager.Application.Interfaces
{
    public interface IBookAppService : IDisposable
    {
        Task<IResult<IEnumerable<BookViewModel>>> GetAllAsync();
        Task<IResult<BookViewModel>> GetByIdAsync(long id);
        Task<IResult<BookViewModel>> AddAsync(BookViewModel bookViewModel);
        Task<IResult<BookViewModel>> UpdateAsync(BookViewModel bookViewModel);
        Task<IResult<long>> RemoveAsync(long id);

        IResult<IEnumerable<BookViewModel>> GetAll();
        IResult<BookViewModel> GetById(long id);
        IResult<BookViewModel> Add(BookViewModel bookViewModel);
        IResult<BookViewModel> Update(BookViewModel bookViewModel);
        IResult<long> Remove(long id);
    }
}
