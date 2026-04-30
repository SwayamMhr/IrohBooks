using Humanizer.Localisation;
using IrohBooks.Data;
using IrohBooks.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace IrohBooks.Controllers
{
    public class ProductController : Controller
    {
        private Repository<Product> products;
        private Repository<Genre> genres;
        private Repository<Category> categories;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            products = new Repository<Product>(context);
            genres = new Repository<Genre>(context);
            categories = new Repository<Category>(context);
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await products.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)

        {
            ViewBag.Genres = await genres.GetAllAsync();
            ViewBag.Categories = await categories.GetAllAsync();
            if (id == 0)
            {
                ViewBag.Operation = "Add";
                return View(new Product());
            }
            else
            {
                Product product = await products.GetByIdAsync(id, new QueryOptions<Product>
                {
                    Includes = "Product Genres. Genre, Category",
                    Where = p => p.ProductId == id
                });
                ViewBag.Operation = "Edit";
                return View(product);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(Product product, int[] genreIds, int catId)
        {
            ViewBag.Genres = await genres.GetAllAsync();
            ViewBag.Categories = await categories.GetAllAsync();
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(fileStream);
                    }
                    product.ImageUrl = uniqueFileName;
                }

                //
                if (product.ProductId == 0)
                {

                    product.CategoryId = catId;
                    //add genres
                    foreach (int id in genreIds)
                    {
                        product.ProductGenres?.Add(new ProductGenre { GenreId = id, ProductId = product.ProductId });
                    }
                    await products.AddAsync(product);
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    var existingProduct = await products.GetByIdAsync(product.ProductId, new QueryOptions<Product> { Includes = "ProductGenres" });

                    if (existingProduct == null)
                    {
                        ModelState.AddModelError("", "Product not found.");
                        ViewBag.Genres = await genres.GetAllAsync();
                        ViewBag.Categories = await categories.GetAllAsync();
                        return View(product);
                    }
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;
                    existingProduct.Stock = product.Stock;
                    existingProduct.CategoryId = catId;

                    // Update product genres
                    existingProduct.ProductGenres?.Clear();
                    foreach (int id in genreIds)
                    {
                        existingProduct.ProductGenres?.Add(new ProductGenre { GenreId = id, ProductId = product.ProductId });
                    }

                    try
                    {
                        await products.UpdateAsync(existingProduct);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Error: {ex.GetBaseException().Message}");
                        ViewBag.Genres = await genres.GetAllAsync();
                        ViewBag.Categories = await categories.GetAllAsync();
                        return View(product);
                    }
                }
            }
            return RedirectToAction("Index", "Product");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await products.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Product not found.");
                return RedirectToAction("Index");

            }

        }
    }
}
