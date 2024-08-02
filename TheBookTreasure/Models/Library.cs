using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheBookTreasure.Models
{
    public class Library
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "שם הספריה")]
        public string LibraryName { get; set; }
        public List<Shelf> Shelfs { get; set; }
        public int GenreId { get; set; }
        [Required]
        [Display(Name ="זאנר")]
        public string Genre { get; set; }
        public Library() 
        { 
            Shelfs = new List<Shelf>();
        }

    }
}
