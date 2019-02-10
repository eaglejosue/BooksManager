namespace BooksManager.Domain.Validations.Book
{
    public class RemoveBookValidation : BookValidation<Entities.Book>
    {
        public RemoveBookValidation()
        {
            ValidateId();
        }
    }
}
