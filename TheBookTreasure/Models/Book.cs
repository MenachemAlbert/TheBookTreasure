using System.ComponentModel.DataAnnotations;

namespace TheBookTreasure.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public int GenreId { get; set; }
        [Required]
        [Display(Name = "זאנר")]
        public string Genre { get; set; }   
        [Required]
        [Display(Name = "שם הספר")]
        public string NameOfBook { get; set; } = string.Empty;
        [Required]
        [Display(Name = "גובה של הספר - סנטימטרים")]
        public int Height { get; set; }
        [Required]
        [Display(Name = "עובי הספר - סנטימטרים")]
        public int Thickness { get; set; }
        public Shelf Shelf { get; set; }

        
    }
}
