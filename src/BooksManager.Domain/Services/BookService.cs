using System.Threading.Tasks;
using BooksManager.Domain.Entities;
using BooksManager.Domain.Interfaces;
using DotNetCore.Objects;
using BooksManager.Domain.Interfaces.Repository;
using System;
using System.Linq;
using System.Net;
using BooksManager.Domain.Exception;
using BooksManager.Domain.Validations.Book;

namespace BooksManager.Domain.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(
            IBookRepository bookRepository,
            IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<IResult<IQueryable<Book>>> GetAllAsync()
        {
            var allBooks = _bookRepository.GetAll();
            return new SuccessResult<IQueryable<Book>>(allBooks).ToTask();
        }

        public Task<IResult<Book>> GetByIdAsync(long id)
        {
            var book = _bookRepository.GetById(id);
            return new SuccessResult<Book>(book).ToTask();
        }

        public Task<IResult<Book>> AddAsync(Book book)
        {
            var bookValidationResult = new AddNewBookValidation().Validate(book);
            if (!bookValidationResult.IsValid)
                return new ErrorResult<Book>(bookValidationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty).ToTask();
            
            var bookEntity = _bookRepository.Add(book);
            if (!_unitOfWork.Commit()) throw new ExceptionHandler(HttpStatusCode.BadRequest, "A problem occurred during saving the data.");
            
            return new SuccessResult<Book>(bookEntity).ToTask();
        }

        public Task<IResult<Book>> UpdateAsync(Book book)
        {
            var bookValidationResult = new UpdateBookValidation().Validate(book);
            if (!bookValidationResult.IsValid)
                return new ErrorResult<Book>(bookValidationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty).ToTask();

            if (_bookRepository.GetById(book.Id) == null)
                throw new ExceptionHandler(HttpStatusCode.NotFound, $"Book id {book.Id} not found.");

            book = _bookRepository.Update(book);

            if (!_unitOfWork.Commit()) throw new ExceptionHandler(HttpStatusCode.BadRequest, "A problem occurred during saving the data.");

            return new SuccessResult<Book>(book).ToTask();
        }

        public Task<IResult<long>> RemoveAsync(long id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null) throw new ExceptionHandler(HttpStatusCode.NotFound, $"Book id {book.Id} not found.");

            var bookValidationResult = new RemoveBookValidation().Validate(book);
            if (!bookValidationResult.IsValid)
                return new ErrorResult<long>(bookValidationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty).ToTask();

            _bookRepository.Remove(id);

            if (!_unitOfWork.Commit()) throw new ExceptionHandler(HttpStatusCode.BadRequest, "A problem occurred during saving the data.");

            return new SuccessResult<long>(id).ToTask();
        }


        public IResult<IQueryable<Book>> GetAll()
        {
            var allBooks = _bookRepository.GetAll();
            return new SuccessResult<IQueryable<Book>>(allBooks);
        }

        public IResult<Book> GetById(long id)
        {
            var book = _bookRepository.GetById(id);
            return new SuccessResult<Book>(book);
        }

        public IResult<Book> Add(Book book)
        {
            var bookValidationResult = new AddNewBookValidation().Validate(book);
            if (!bookValidationResult.IsValid)
                return new ErrorResult<Book>(bookValidationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);

            var bookEntity = _bookRepository.Add(book);
            if (!_unitOfWork.Commit()) throw new ExceptionHandler(HttpStatusCode.BadRequest, "A problem occurred during saving the data.");

            return new SuccessResult<Book>(bookEntity);
        }

        public IResult<Book> Update(Book book)
        {
            var bookValidationResult = new UpdateBookValidation().Validate(book);
            if (!bookValidationResult.IsValid)
                return new ErrorResult<Book>(bookValidationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);

            if (_bookRepository.GetById(book.Id) == null)
                throw new ExceptionHandler(HttpStatusCode.NotFound, $"Book id {book.Id} not found.");

            book = _bookRepository.Update(book);

            if (!_unitOfWork.Commit()) throw new ExceptionHandler(HttpStatusCode.BadRequest, "A problem occurred during saving the data.");

            return new SuccessResult<Book>(book);
        }

        public IResult<long> Remove(long id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null) throw new ExceptionHandler(HttpStatusCode.NotFound, $"Book id {book.Id} not found.");

            var bookValidationResult = new RemoveBookValidation().Validate(book);
            if (!bookValidationResult.IsValid)
                return new ErrorResult<long>(bookValidationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);

            _bookRepository.Remove(id);

            if (!_unitOfWork.Commit()) throw new ExceptionHandler(HttpStatusCode.BadRequest, "A problem occurred during saving the data.");

            return new SuccessResult<long>(id);
        }



        public void Dispose() => GC.SuppressFinalize(this);
    }
}
