using TheBookTreasure.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace TheBookTreasure.DAL
{
    public class DataLayer : DbContext
    {
        public DataLayer(string ConnectionString) : base(GetOptions(ConnectionString))
        {
            Database.EnsureCreated();
            Seed();
        }
        private void Seed()
        {
            if (!genres.Any())
            {
                Genre genre1 = new Genre { Name = "תורה"};
                Genre genre2 = new Genre { Name = "הלכה" };
                Genre genre3 = new Genre { Name = "מוסר" };
                Genre genre4 = new Genre { Name = "חסידות" };
                genres.Add(genre1);
                genres.Add(genre2);
                genres.Add(genre3);
                genres.Add(genre4);
                SaveChanges();

            }
            if (!Librarys.Any())
            {
                Library library = new Library { Genre = "תורה" ,GenreId = 1, LibraryName = "ספרית תורה 1" };
                Librarys.Add(library);
                SaveChanges();
            }
            if (!Shelfs.Any())
            {
                var library = Librarys.FirstOrDefault();
                if (library != null)
                {
                    Shelf shelf = new Shelf { Height = 50, Width = 100,Library = library };
                    Shelfs.Add(shelf);
                    SaveChanges();
                }
            }
            if (!Books.Any()) 
            {
                var shelf = Shelfs.FirstOrDefault();
                if (shelf != null)
                {
                    Book book = new Book { Genre = "תורה", GenreId = 1, NameOfBook = "חמשה חומשי תורה", Height =38, Thickness =15, Shelf = shelf };
                    Books.Add(book);
                    SaveChanges();
                }
            }
        }
        private static DbContextOptions GetOptions(string ConnectionString)
        {
            return new DbContextOptionsBuilder().UseSqlServer(ConnectionString).Options;
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Library> Librarys { get; set; }
        public DbSet<Shelf> Shelfs { get; set; }
        public DbSet<Genre> genres { get; set; }

    }
}