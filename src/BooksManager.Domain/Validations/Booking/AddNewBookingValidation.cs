namespace BooksManager.Domain.Validations.Book
{
    public class AddNewBookingValidation : BookingValidation<Entities.Booking>
    {
        public AddNewBookingValidation()
        {
            ValidateBook();
            ValidateBookId();
            ValidateBookingPeriod();
            ValidatePrice();
        }
    }
}
