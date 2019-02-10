using System.Collections.Generic;
using System.Threading.Tasks;
using BooksManager.Application.Interfaces;
using BooksManager.Application.ViewModels;
using DotNetCore.Objects;
using System;
using AutoMapper;
using BooksManager.Domain.Interfaces;
using BooksManager.Domain.Entities;

namespace BooksManager.Application.Services
{
    public class BookAppService : IBookAppService
    {
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;

        public BookAppService(
            IMapper mapper,
            IBookService bookService)
        {
            _mapper = mapper;
            _bookService = bookService;
        }

        public async Task<IResult<IEnumerable<BookViewModel>>> GetAllAsync()
        {
            var allBooksResult = await _bookService.GetAllAsync();
            var allBookViewModels = allBooksResult.Data.ProjectTo<BookViewModel>();
            return new SuccessResult<IEnumerable<BookViewModel>>(allBookViewModels);
        }

        public async Task<IResult<BookViewModel>> GetByIdAsync(long id)
        {
            var customerResult = await _bookService.GetByIdAsync(id);
            var bookViewModel = _mapper.Map<BookViewModel>(customerResult.Data);
            return new SuccessResult<BookViewModel>(bookViewModel);
        }

        public async Task<IResult<BookViewModel>> AddAsync(BookViewModel bookViewModel)
        {
            var book = _mapper.Map<Book>(bookViewModel);
            var bookResult = await _bookService.AddAsync(book);
            bookViewModel = _mapper.Map<BookViewModel>(bookResult.Data);
            return new SuccessResult<BookViewModel>(bookViewModel);
        }

        public async Task<IResult<BookViewModel>> UpdateAsync(BookViewModel bookViewModel)
        {
            var book = _mapper.Map<Book>(bookViewModel);
            var bookResult = await _bookService.UpdateAsync(book);
            bookViewModel = _mapper.Map<BookViewModel>(bookResult.Data);
            return new SuccessResult<BookViewModel>(bookViewModel);
        }

        public Task<IResult<long>> RemoveAsync(long id)
        {
            return _bookService.RemoveAsync(id);
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
