using BooksManager.Domain.Entities;
using BooksManager.Domain.ValueObjects;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksManager.Application.ViewModels
{
    public class BookingViewModel
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "The BookingPeriod is Required")]
        [DisplayName("BookingPeriod")]
        public BookingPeriod BookingPeriod { get; set; }

        [Required(ErrorMessage = "The Book is Required")]
        [DisplayName("Book")]
        public Book Book { get; set; }

        [Required(ErrorMessage = "The BookId is Required")]
        [DisplayName("BookId")]
        public long BookId { get; set; }

        [Required(ErrorMessage = "The BirthDate is Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "${0:#,0}")]
        [DataType(DataType.Currency, ErrorMessage = "Price em formato inválido")]
        [DisplayName("Price")]
        public decimal Price { get; set; }
    }
}
