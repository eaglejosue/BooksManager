namespace BooksManager.Domain.Validations.Customer
{
    public class AddNewBookValidation : BookValidation<Entities.Customer>
    {
        public AddNewBookValidation()
        {
            ValidateName();
            ValidateTelephone();
            ValidateBirthDate();
            ValidateEmail();
        }
    }
}
