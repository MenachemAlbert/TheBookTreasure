using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheBookTreasure.Models;

namespace TheBookTreasure.Controllers
{
    public class LibrarysController : Controller
    {
        public IActionResult Index()
        {
            return View(DAL.Data.Get.Librarys.ToList());
        }
        public IActionResult Details(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            Library library = DAL.Data.Get.Librarys.Include(l => l.Shelfs).FirstOrDefault(l => l.Id == id); 
            if (library == null) return RedirectToAction("Index");
            return View(library);
        }

        public IActionResult Create()
        {
            ViewData["genres"] = new SelectList(DAL.Data.Get.genres, "Id", "Name");
            return View(new Library());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Library library)
        {
            library.Genre = DAL.Data.Get.genres.First(g => g.Id == library.GenreId).Name;
            DAL.Data.Get.Add(library);
            DAL.Data.Get.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
