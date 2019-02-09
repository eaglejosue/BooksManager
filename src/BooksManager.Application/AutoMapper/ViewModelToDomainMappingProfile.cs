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
        }
    }
}