namespace BooksManager.Domain.Validations.Book
{
    public class UpdateBookingValidation : BookingValidation<Entities.Booking>
    {
        public UpdateBookingValidation()
        {
            ValidateId();
            ValidateBook();
            ValidateBookId();
            ValidateBookingPeriod();
            ValidatePrice();
        }
    }
}
