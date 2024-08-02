using System.ComponentModel.DataAnnotations;

namespace TheBookTreasure.Models
{
    public class Shelf
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "גובה המדף - סנטימטרים")]
        public int Height { get; set; }
        [Required]
        [Display(Name = "רוחב המדף - סנטימטרים")]
        public int Width { get; set; } 
        public List<Book> Books { get; set; }
        [Required]
        public Library Library { get; set; }

        public Shelf()
        {
            Books = new List<Book>();
        }
    }
}
