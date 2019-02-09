namespace BooksManager.Domain.Validations.Customer
{
    public class AddNewCustomerValidation : CustomerValidation<Entities.Customer>
    {
        public AddNewCustomerValidation()
        {
            ValidateName();
            ValidateTelephone();
            ValidateBirthDate();
            ValidateEmail();
        }
    }
}
