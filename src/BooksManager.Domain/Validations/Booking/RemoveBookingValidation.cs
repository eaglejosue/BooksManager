namespace BooksManager.Domain.Validations.Book
{
    public class RemoveBookingValidation : BookingValidation<Entities.Booking>
    {
        public RemoveBookingValidation()
        {
            ValidateId();
        }
    }
}
