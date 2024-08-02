using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBookTreasure.Models;

namespace TheBookTreasure.Controllers
{
    public class ShelfesController : Controller
    {
        public IActionResult Index(int? libraryId)
        {
            if (libraryId == null) return RedirectToAction("Index");
            var shelves = DAL.Data.Get.Shelfs.Where(s => s.Library.Id == libraryId).ToList();
            //if (shelves.Count == 0) return RedirectToAction("Index", new { libraryId = libraryId});
            ViewData["libraryId"] = libraryId;
            return View(shelves);
        }
        public IActionResult Details(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            Shelf shelf = DAL.Data.Get.Shelfs.Include(l => l.Books).FirstOrDefault(l => l.Id == id);
            if (shelf == null) return RedirectToAction("Index");
            ViewData["libraryId"] = shelf.Library.Id;
            ViewData["freeWidth"] = shelf.Width - shelf.Books.Sum(b => b.Thickness);

            return View(shelf);
        }
        public IActionResult Create(int? libraryId)
        {
            if (libraryId == null) return RedirectToAction("Index");
            Library library = DAL.Data.Get.Librarys.Find(libraryId);
            if (library == null) return RedirectToAction("Index");
            ViewData["libraryId"] = libraryId;
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(int library_id, [Bind("Height,Width")] Shelf shelf)
        {
            Library library = DAL.Data.Get.Librarys.Find(library_id);
            if (library == null) return RedirectToAction("Index");
            shelf.Library = library;
            DAL.Data.Get.Shelfs.Add(shelf);
            DAL.Data.Get.SaveChanges();
            return RedirectToAction("Index", new { libraryId = library_id });

        }
    }
}
