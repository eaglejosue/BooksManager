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
            var allCustomers = _customerRepository.GetAll();
            return new SuccessResult<IQueryable<Customer>>(allCustomers).ToTask();
        }

        public Task<IResult<Customer>> GetByIdAsync(long id)
        {
            var customer = _customerRepository.GetById(id);
            return new SuccessResult<Customer>(customer).ToTask();
        }

        public Task<IResult<Customer>> AddAsync(Customer customer)
        {
            var customerValidationResult = new AddNewBookValidation().Validate(customer);
            if (!customerValidationResult.IsValid)
                return new ErrorResult<Customer>(customerValidationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty).ToTask();

            var customerEntity = _customerRepository.Add(customer);
            if (!_unitOfWork.Commit()) throw new ExceptionHandler(HttpStatusCode.BadRequest, "A problem occurred during saving the data.");

            return new SuccessResult<Customer>(customerEntity).ToTask();
        }

        public Task<IResult<Customer>> UpdateAsync(Customer customer)
        {
            var customerValidationResult = new UpdateBookValidation().Validate(customer);
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

            var customerValidationResult = new RemoveBookValidation().Validate(customer);
            if (!customerValidationResult.IsValid)
                return new ErrorResult<long>(customerValidationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty).ToTask();

            _customerRepository.Remove(id);

            if (!_unitOfWork.Commit()) throw new ExceptionHandler(HttpStatusCode.BadRequest, "A problem occurred during saving the data.");

            return new SuccessResult<long>(id).ToTask();
        }


        public IResult<IQueryable<Customer>> GetAll()
        {
            var allCustomers = _customerRepository.GetAll();
            return new SuccessResult<IQueryable<Customer>>(allCustomers);
        }

        public IResult<Customer> GetById(long id)
        {
            var customer = _customerRepository.GetById(id);
            return new SuccessResult<Customer>(customer);
        }

        public IResult<Customer> Add(Customer customer)
        {
            var customerValidationResult = new AddNewBookValidation().Validate(customer);
            if (!customerValidationResult.IsValid)
                return new ErrorResult<Customer>(customerValidationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);

            var customerEntity = _customerRepository.Add(customer);
            if (!_unitOfWork.Commit()) throw new ExceptionHandler(HttpStatusCode.BadRequest, "A problem occurred during saving the data.");

            return new SuccessResult<Customer>(customerEntity);
        }

        public IResult<Customer> Update(Customer customer)
        {
            var customerValidationResult = new UpdateBookValidation().Validate(customer);
            if (!customerValidationResult.IsValid)
                return new ErrorResult<Customer>(customerValidationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);

            if (_customerRepository.GetById(customer.Id) == null)
                throw new ExceptionHandler(HttpStatusCode.NotFound, $"Customer id {customer.Id} not found.");

            customer = _customerRepository.Update(customer);

            if (!_unitOfWork.Commit()) throw new ExceptionHandler(HttpStatusCode.BadRequest, "A problem occurred during saving the data.");

            return new SuccessResult<Customer>(customer);
        }

        public IResult<long> Remove(long id)
        {
            var customer = _customerRepository.GetById(id);
            if (customer == null) throw new ExceptionHandler(HttpStatusCode.NotFound, $"Customer id {customer.Id} not found.");

            var customerValidationResult = new RemoveBookValidation().Validate(customer);
            if (!customerValidationResult.IsValid)
                return new ErrorResult<long>(customerValidationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);

            _customerRepository.Remove(id);

            if (!_unitOfWork.Commit()) throw new ExceptionHandler(HttpStatusCode.BadRequest, "A problem occurred during saving the data.");

            return new SuccessResult<long>(id);
        }



        public void Dispose() => GC.SuppressFinalize(this);
    }
}
