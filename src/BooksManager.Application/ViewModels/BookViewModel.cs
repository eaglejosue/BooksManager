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
        public string Title { get; set; }

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

        [Required(ErrorMessage = "The Author is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Author")]
        public string Author { get; set; }

        [Required(ErrorMessage = "The Year is Required")]
        [DisplayName("Year")]
        public int Year { get; set; }

        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Publisher")]
        public string Publisher { get; set; }

        [Required(ErrorMessage = "The Edition is Required")]
        [DisplayName("Edition")]
        public int Edition { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        [DisplayName("Tag")]
        public string Tag { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        [DisplayName("Summary")]
        public string Summary { get; set; }

        //[DisplayName("Image")]
        //public byte[] Image { get; private set; }
    }
}
