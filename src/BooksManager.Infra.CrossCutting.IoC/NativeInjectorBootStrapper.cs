using AutoMapper;
using BooksManager.Application.Interfaces;
using BooksManager.Application.Services;
using BooksManager.Domain.Interfaces;
using BooksManager.Domain.Interfaces.Repository;
using BooksManager.Domain.Services;
using BooksManager.Infra.Data.Context;
using BooksManager.Infra.Data.Repository;
using BooksManager.Infra.Data.UoW;
using Microsoft.Extensions.DependencyInjection;

namespace BooksManager.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));
            services.AddScoped<ICustomerAppService, CustomerAppService>();

            // Domain
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IBookService, BookService>();

            // Infra - Data
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<BooksManagerContext>();
        }
    }
}
