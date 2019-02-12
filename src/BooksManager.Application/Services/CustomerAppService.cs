using AutoMapper;
using AutoMapper.QueryableExtensions;
using BooksManager.Application.Interfaces;
using BooksManager.Application.ViewModels;
using BooksManager.Domain.Entities;
using BooksManager.Domain.Interfaces;
using DotNetCore.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksManager.Application.Services
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;

        public CustomerAppService(
            IMapper mapper,
            ICustomerService customerService)
        {
            _mapper = mapper;
            _customerService = customerService;
        }

        public async Task<IResult<IEnumerable<CustomerViewModel>>> GetAllAsync()
        {
            var allCustomersResult = await _customerService.GetAllAsync();
            var allCustomerViewModels = allCustomersResult.Data.ProjectTo<CustomerViewModel>();
            return new SuccessResult<IEnumerable<CustomerViewModel>>(allCustomerViewModels);
        }

        public async Task<IResult<CustomerViewModel>> GetByIdAsync(long id)
        {
            var customerResult = await _customerService.GetByIdAsync(id);
            var customerViewModel = _mapper.Map<CustomerViewModel>(customerResult.Data);
            return new SuccessResult<CustomerViewModel>(customerViewModel);
        }

        public async Task<IResult<CustomerViewModel>> AddAsync(CustomerViewModel customerViewModel)
        {
            var customer = _mapper.Map<Customer>(customerViewModel);
            var customerResult = await _customerService.AddAsync(customer);
            customerViewModel = _mapper.Map<CustomerViewModel>(customerResult.Data);
            return new SuccessResult<CustomerViewModel>(customerViewModel);
        }

        public async Task<IResult<CustomerViewModel>> UpdateAsync(CustomerViewModel customerViewModel)
        {
            var customer = _mapper.Map<Customer>(customerViewModel);
            var customerResult = await _customerService.UpdateAsync(customer);
            customerViewModel = _mapper.Map<CustomerViewModel>(customerResult.Data);
            return new SuccessResult<CustomerViewModel>(customerViewModel);
        }

        public Task<IResult<long>> RemoveAsync(long id)
        {
            return _customerService.RemoveAsync(id);
        }


        public IResult<IEnumerable<CustomerViewModel>> GetAll()
        {
            var allCustomersResult = _customerService.GetAll();
            var allCustomerViewModels = allCustomersResult.Data.ProjectTo<CustomerViewModel>();
            return new SuccessResult<IEnumerable<CustomerViewModel>>(allCustomerViewModels);
        }

        public IResult<CustomerViewModel> GetById(long id)
        {
            var customerResult = _customerService.GetById(id);
            var customerViewModel = _mapper.Map<CustomerViewModel>(customerResult.Data);
            return new SuccessResult<CustomerViewModel>(customerViewModel);
        }

        public IResult<CustomerViewModel> Add(CustomerViewModel customerViewModel)
        {
            var customer = _mapper.Map<Customer>(customerViewModel);
            var customerResult = _customerService.Add(customer);
            customerViewModel = _mapper.Map<CustomerViewModel>(customerResult.Data);
            return new SuccessResult<CustomerViewModel>(customerViewModel);
        }

        public IResult<CustomerViewModel> Update(CustomerViewModel customerViewModel)
        {
            var customer = _mapper.Map<Customer>(customerViewModel);
            var customerResult = _customerService.Update(customer);
            customerViewModel = _mapper.Map<CustomerViewModel>(customerResult.Data);
            return new SuccessResult<CustomerViewModel>(customerViewModel);
        }

        public IResult<long> Remove(long id)
        {
            return _customerService.Remove(id);
        }



        public void Dispose() => GC.SuppressFinalize(this);
    }
}
