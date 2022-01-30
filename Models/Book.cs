using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Book
    {
        public int id { set; get; }

        [Display(Name = "Назва")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        // [MaxLength(50)]
        public string title { set; get; }

        [Display(Name = "Автор")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        // [MaxLength(50)]
        public string author { set; get; }

        [Display(Name = "Жанр")]
        [Required]
        [MaxLength(20)]
        public string? genre { set; get; }
        
        [Display(Name = "Рік")]
        // [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        public DateTime year { set; get; }

        [Display(Name = "Наявність")]
        public bool isBusy { set; get; }
    }
}