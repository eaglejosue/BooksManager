namespace BooksManager.Domain.Validations.Booking
{
    public class RemoveBookingValidation : BookingValidation<Entities.Booking>
    {
        public RemoveBookingValidation()
        {
            ValidateId();
        }
    }
}
