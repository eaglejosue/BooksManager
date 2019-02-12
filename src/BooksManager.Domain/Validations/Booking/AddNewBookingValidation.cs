namespace BooksManager.Domain.Validations.Booking
{
    public class AddNewBookingValidation : BookingValidation<Entities.Booking>
    {
        public AddNewBookingValidation()
        {
            ValidateBook();
            ValidateBookId();
            ValidateCustomerId();
            ValidateBookingPeriod();
            ValidatePrice();
        }
    }
}
