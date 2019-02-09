namespace BooksManager.Domain.Validations.Customer
{
    public class UpdateCustomerValidation : CustomerValidation<Entities.Customer>
    {
        public UpdateCustomerValidation()
        {
            ValidateId();
            ValidateName();
            ValidateTelephone();
            ValidateBirthDate();
            ValidateEmail();
        }
    }
}
