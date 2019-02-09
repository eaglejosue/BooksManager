using AutoMapper;
using BooksManager.Application.ViewModels;
using BooksManager.Domain.Entities;

namespace BooksManager.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
        }
    }
}