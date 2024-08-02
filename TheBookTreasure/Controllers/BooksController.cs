using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheBookTreasure.Models;

namespace TheBookTreasure.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            ViewData["genres"] = new SelectList(DAL.Data.Get.genres, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Book book)
        {
            book.Genre = DAL.Data.Get.genres.First(g => g.Id == book.GenreId).Name;
            Shelf shelf = FindShelf(book);
            if (shelf == null)
            {
                TempData["msg"] = "לא נמצא מדף שעונה על הדרישות";
                return RedirectToAction("Index" , "Home");
            }
            book.Shelf = shelf;
            DAL.Data.Get.Books.Add(book);
            DAL.Data.Get.SaveChanges();
            if (shelf.Height - book.Height >= 10) TempData["msg"] = "הכנסת ספר שקטן בגובהו מהמדף יותר מ -  10 סנטימטר";
            return RedirectToAction("Details", "Shelfes", new { id = book.Shelf.Id });

        }
        public Shelf FindShelf(Book book)
        {
            var shelves = DAL.Data.Get.Shelfs.Include(s => s.Books).Include(s => s.Library)
                 .Where(s => s.Library.Genre == book.Genre).ToList();
            foreach (var shelf in shelves)
            {
                if (book.Height <= shelf.Height)
                {
                    int totalThickness = shelf.Books.Sum(b => b.Thickness) + book.Thickness;
                    if (totalThickness <= shelf.Width)
                    {
                        return shelf;
                    }
                }
            }
            return null;
        }

        public IActionResult CreateBookSet()
        {

            var books = new List<Book> { new Book() };
            ViewData["genres"] = new SelectList(DAL.Data.Get.genres, "Id", "Name");
            return View(books);
        }

        [HttpPost]
        public IActionResult CreateBookSet(List<Book> Books)
        {
          
            Shelf shelf = FindShelfForSet(Books);
            if (shelf == null)
            {
                TempData["msg"] = "לא נמצא מדף שעונה על הדרישות";
                return RedirectToAction("Index", "Home");
            }

            foreach (var book in Books)
            {
                book.Genre = DAL.Data.Get.genres.First(g => g.Id == Books[0].GenreId).Name;
                book.Shelf = shelf;
                DAL.Data.Get.Books.Add(book);
            }
            DAL.Data.Get.SaveChanges();
			if ((shelf.Height - Books.Min(b => b.Height)) >= 10) TempData["msg"] = "הכנסת ספר שקטן בגובהו מהמדף יותר מ -  10 סנטימטר";
			return RedirectToAction("Details", "Shelfes", new { id = shelf.Id });
        }

        private Shelf FindShelfForSet(List<Book> books)
        {
            var genre = DAL.Data.Get.genres.First(g => g.Id == books[0].GenreId).Name;;
            var MaxHeight = books.Max(b => b.Height);
            var SumOfThickness = books.Sum(b => b.Thickness);                        
            var shelves = DAL.Data.Get.Shelfs.Include(s => s.Books).Include(s => s.Library)
                 .Where(s => s.Library.Genre == genre).ToList();

            foreach (var shelf in shelves)
            {
                if (MaxHeight <= shelf.Height)
                {
                    int currentThickness = shelf.Books.Sum(b => b.Thickness);
                    if (currentThickness + SumOfThickness <= shelf.Width)
                    {
                        return shelf;
                    }
                }
            }
            return null;
        }
    }
}
