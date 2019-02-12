namespace BooksManager.Domain.Validations.Booking
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
