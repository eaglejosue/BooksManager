using System.Threading.Tasks;
using BooksManager.Domain.Entities;
using BooksManager.Domain.Interfaces;
using DotNetCore.Objects;
using BooksManager.Domain.Interfaces.Repository;
using System;
using System.Linq;
using BooksManager.Domain.Validations.Customer;
using System.Net;
using BooksManager.Domain.Exception;

namespace BooksManager.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(
            ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<IResult<IQueryable<Customer>>> GetAllAsync()
        {
            var customer = _customerRepository.GetAll();
            return new SuccessResult<IQueryable<Customer>>(customer).ToTask();
        }

        public Task<IResult<Customer>> GetByIdAsync(long id)
        {
            var customer = _customerRepository.GetById(id);
            return new SuccessResult<Customer>(customer).ToTask();
        }

        public Task<IResult<Customer>> AddAsync(Customer customer)
        {
            var customerValidationResult = new AddNewCustomerValidation().Validate(customer);
            if (!customerValidationResult.IsValid)
                return new ErrorResult<Customer>(customerValidationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty).ToTask();

            var customerEntity = _customerRepository.Add(customer);
            if (!_unitOfWork.Commit()) throw new ExceptionHandler(HttpStatusCode.BadRequest, "A problem occurred during saving the data.");

            return new SuccessResult<Customer>(customerEntity).ToTask();
        }

        public Task<IResult<Customer>> UpdateAsync(Customer customer)
        {
            var customerValidationResult = new UpdateCustomerValidation().Validate(customer);
            if (!customerValidationResult.IsValid)
                return new ErrorResult<Customer>(customerValidationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty).ToTask();

            if (_customerRepository.GetById(customer.Id) == null)
                throw new ExceptionHandler(HttpStatusCode.NotFound, $"Customer id {customer.Id} not found.");

            customer = _customerRepository.Update(customer);

            if (!_unitOfWork.Commit()) throw new ExceptionHandler(HttpStatusCode.BadRequest, "A problem occurred during saving the data.");

            return new SuccessResult<Customer>(customer).ToTask();
        }

        public Task<IResult<long>> RemoveAsync(long id)
        {
            var customer = _customerRepository.GetById(id);
            if (customer == null) throw new ExceptionHandler(HttpStatusCode.NotFound, $"Customer id {customer.Id} not found.");

            var customerValidationResult = new RemoveCustomerValidation().Validate(customer);
            if (!customerValidationResult.IsValid)
                return new ErrorResult<long>(customerValidationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty).ToTask();

            _customerRepository.Remove(id);

            if (!_unitOfWork.Commit()) throw new ExceptionHandler(HttpStatusCode.BadRequest, "A problem occurred during saving the data.");

            return new SuccessResult<long>(id).ToTask();
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
