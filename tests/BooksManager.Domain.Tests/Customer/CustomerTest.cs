using BooksManager.Domain.Interfaces;
using BooksManager.Domain.Tests.StaticClasses;
using DotNetCore.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BooksManager.Domain.Tests.Customer
{
    [TestClass]
    public class CustomerTest
    {
        private readonly Entities.Customer _customerToAddValid;
        private readonly Mock<ICustomerService> _mock;

        public CustomerTest()
        {
            _customerToAddValid =
                new Entities.Customer(
                    0,
                    "New Customer",
                    "newCustomer@email.com",
                    "051017991234567",
                    DateTime.Today
                    );

            // Inicia Mock
            _mock = new Mock<ICustomerService>(MockBehavior.Strict);

            // Define retorno válido no Mock
            _mock.Setup(c => c.AddAsync(_customerToAddValid))
                .Returns(() => new SuccessResult<Entities.Customer>(_customerToAddValid).ToTask());
        }

        [TestMethod]
        public async void NewCustomerIsValid()
        {
            IResult<Entities.Customer> customerResult = null;
            Entities.Customer customerActual = null;

            try
            {
                customerResult = await _mock.Object.AddAsync(_customerToAddValid);
                customerActual = customerResult.Data;
            }
            catch (System.Exception ex)
            {
                StaticExceptionFail.New(ex);
            }

            Assert.IsNotNull(customerResult);
            Assert.AreEqual(true, customerResult.Success);

            Assert.AreNotEqual(_customerToAddValid.Id, customerActual.Id);
            Assert.AreEqual(_customerToAddValid.Name, customerActual.Name);
            Assert.AreEqual(_customerToAddValid.Email, customerActual.Email);
            Assert.AreEqual(_customerToAddValid.Telephone, customerActual.Telephone);
            Assert.AreEqual(_customerToAddValid.BirthDate.Date, customerActual.BirthDate.Date);
        }
    }
}
