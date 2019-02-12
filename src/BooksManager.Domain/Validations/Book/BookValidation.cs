using FluentValidation;
namespace BooksManager.Domain.Validations.Book
{
    public abstract class BookValidation<T> : AbstractValidator<T> where T : Entities.Book
    {
        protected void ValidateName()
        {
            RuleFor(c => c.Title)
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(2, 100).WithMessage("The Name must have between 2 and 100 characters");
        }

        protected void ValidateDescription()
        {
            RuleFor(b => b.Description)
                .NotEmpty();
        }

        protected void ValidatePrice()
        {
            RuleFor(b => b.Price)
                .GreaterThan(decimal.Zero);
        }

        protected void ValidateId()
        {
            RuleFor(b => b.Id)
                .NotEqual(0);
        }
    }
}
