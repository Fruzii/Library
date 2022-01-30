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
        public string author { set; get; }

        [Display(Name = "Жанр")]
        [Required]
        [MaxLength(20)]
        public string? genre { set; get; }
        
        [Display(Name = "Рік")]
        public int year { set; get; }

        [Display(Name = "Зайнята")]
        public bool isBusy { set; get; }
    }
}