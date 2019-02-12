using FluentValidation;

namespace BooksManager.Domain.Validations.Book
{
    public abstract class BookingValidation<T> : AbstractValidator<T> where T : Entities.Booking
    {
        protected void ValidateBookingPeriod()
        {
            RuleFor(c => c.BookingPeriod)
                .NotNull().NotEmpty()
                .WithMessage("Please ensure you have selected the BookingPeriod");
        }

        protected void ValidateBook()
        {
            RuleFor(c => c.Book)
                .NotNull().NotEmpty()
                .WithMessage("Please ensure you have selected the Book");
        }

        protected void ValidatePrice()
        {
            RuleFor(b => b.Price)
                .NotEqual(decimal.Zero);
        }

        protected void ValidateBookId()
        {
            RuleFor(b => b.BookId)
                .NotEqual(0);
        }

        protected void ValidateId()
        {
            RuleFor(b => b.Id)
                .NotEqual(0);
        }
    }
}
