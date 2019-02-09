namespace BooksManager.Domain.Validations.Customer
{
    public class RemoveCustomerValidation : CustomerValidation<Entities.Customer>
    {
        public RemoveCustomerValidation()
        {
            ValidateId();
        }
    }
}
