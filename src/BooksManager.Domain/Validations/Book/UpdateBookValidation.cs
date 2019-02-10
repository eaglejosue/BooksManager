namespace BooksManager.Domain.Validations.Book
{
    public class UpdateBookValidation : BookValidation<Entities.Book>
    {
        public UpdateBookValidation()
        {
            ValidateId();
            ValidateName();
            ValidateDescription();
            ValidatePrice();
        }
    }
}
