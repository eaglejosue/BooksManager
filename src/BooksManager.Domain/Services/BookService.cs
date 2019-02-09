using BooksManager.Domain.Interfaces;
using System;

namespace BooksManager.Domain.Services
{
    public class BookService : IBookService
    {


        public void Dispose() => GC.SuppressFinalize(this);
    }
}
