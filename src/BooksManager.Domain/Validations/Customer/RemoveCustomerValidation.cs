namespace BooksManager.Domain.Validations.Customer
{
    public class RemoveBookValidation : BookValidation<Entities.Customer>
    {
        public RemoveBookValidation()
        {
            ValidateId();
        }
    }
}
