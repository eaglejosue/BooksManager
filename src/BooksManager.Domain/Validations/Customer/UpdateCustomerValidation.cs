namespace BooksManager.Domain.Validations.Customer
{
    public class UpdateBookValidation : BookValidation<Entities.Customer>
    {
        public UpdateBookValidation()
        {
            ValidateId();
            ValidateName();
            ValidateTelephone();
            ValidateBirthDate();
            ValidateEmail();
        }
    }
}
