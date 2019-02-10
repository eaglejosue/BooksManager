using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksManager.Application.ViewModels
{
    public class BookViewModel
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Description is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The BirthDate is Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "${0:#,0}")]
        [DataType(DataType.Currency, ErrorMessage = "Price em formato inválido")]
        [DisplayName("Price")]
        public decimal Price { get; set; }
    }
}
