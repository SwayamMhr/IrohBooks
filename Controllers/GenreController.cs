using Humanizer.Localisation;
using IrohBooks.Data;
using IrohBooks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IrohBooks.Controllers
{
    public class GenreController : Controller
    {
        private Repository<Genre> genres;

        public GenreController(ApplicationDbContext context)
        {
            genres = new Repository<Genre>(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await genres.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await genres.GetByIdAsync(id, new QueryOptions<Genre>() { Includes = "Product Genres. Product" }));
        }

        //Genre/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("GenreId, Name")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                await genres.AddAsync(genre);
                return RedirectToAction("Index");
            }
            return View(genre);

        }

        //Genre/Delete
        [HttpGet]

        public async Task<IActionResult> Delete(int id)
        {
            return View(await genres.GetByIdAsync(id, new QueryOptions<Genre> { Includes = "ProductGenres. Product" }));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(Genre genre)
        {
            await genres.DeleteAsync(genre.GenreId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await genres.GetByIdAsync(id, new QueryOptions<Genre> { Includes = "ProductGenres. Product" }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                await genres.UpdateAsync(genre);
                return RedirectToAction("Index");
            }
            return View(genre);

        }

    }
}

