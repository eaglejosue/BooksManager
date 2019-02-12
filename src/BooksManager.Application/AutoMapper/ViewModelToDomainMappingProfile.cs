using AutoMapper;
using BooksManager.Application.ViewModels;
using BooksManager.Domain.Entities;

namespace BooksManager.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CustomerViewModel, Customer>()
                .ConstructUsing(c => new Customer(c.Id, c.Name, c.Email, c.Telephone, c.BirthDate));

            CreateMap<BookViewModel, Book>()
                .ConstructUsing(b => new Book(b.Id, b.Title, b.Description, b.Price, b.Author,
                    b.Year, b.Publisher, b.Edition, b.Tag, b.Summary));

            CreateMap<BookingViewModel, Booking>()
                .IgnoreAllSourcePropertiesWithAnInaccessibleSetter();
        }
    }
}