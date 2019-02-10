namespace BooksManager.Domain.Validations.Book
{
    public class AddNewBookValidation : BookValidation<Entities.Book>
    {
        public AddNewBookValidation()
        {
            ValidateName();
            ValidateDescription();
            ValidatePrice();
        }
    }
}
