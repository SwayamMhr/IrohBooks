using IrohBooks.Data;
using IrohBooks.Models;
using IrohBooks.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace IrohBooks.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly Repository<Product> _products;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _products = new Repository<Product>(context);
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCardDto>>> GetAll()
        {
            var products = await _products.GetAllAsync(new QueryOptions<Product>
            {
                Includes = "Category,ProductGenres.Genre"
            });

            return Ok(products.Select(ToCardDto));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductCardDto>> GetById(int id)
        {
            var product = await _products.GetByIdAsync(id, new QueryOptions<Product>
            {
                Includes = "Category,ProductGenres.Genre"
            });

            if (product == null)
            {
                return NotFound();
            }

            return Ok(ToCardDto(product));
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ProductCardDto>> Create([FromForm] ProductWriteDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId
            };

            product.ImageUrl = await SaveImageAsync(dto.ImageFile) ?? product.ImageUrl;

            foreach (int genreId in dto.GenreIds)
            {
                product.ProductGenres?.Add(new ProductGenre { GenreId = genreId });
            }

            await _products.AddAsync(product);

            var created = await _products.GetByIdAsync(product.ProductId, new QueryOptions<Product>
            {
                Includes = "Category,ProductGenres.Genre"
            });

            return CreatedAtAction(nameof(GetById), new { id = product.ProductId }, ToCardDto(created!));
        }

        [HttpPut("{id:int}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ProductCardDto>> Update(int id, [FromForm] ProductWriteDto dto)
        {
            var existing = await _products.GetByIdAsync(id, new QueryOptions<Product>
            {
                Includes = "ProductGenres,Category,ProductGenres.Genre"
            });

            if (existing == null)
            {
                return NotFound();
            }

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.Price = dto.Price;
            existing.Stock = dto.Stock;
            existing.CategoryId = dto.CategoryId;

            var savedImage = await SaveImageAsync(dto.ImageFile);
            if (savedImage != null)
            {
                existing.ImageUrl = savedImage;
            }

            existing.ProductGenres?.Clear();
            foreach (int genreId in dto.GenreIds)
            {
                existing.ProductGenres?.Add(new ProductGenre { GenreId = genreId, ProductId = id });
            }

            await _products.UpdateAsync(existing);

            var updated = await _products.GetByIdAsync(id, new QueryOptions<Product>
            {
                Includes = "Category,ProductGenres.Genre"
            });

            return Ok(ToCardDto(updated!));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _products.GetByIdAsync(id, new QueryOptions<Product>());
            if (existing == null)
            {
                return NotFound();
            }

            await _products.DeleteAsync(id);
            return NoContent();
        }

        private static ProductCardDto ToCardDto(Product product)
        {
            var genres = product.ProductGenres ?? Array.Empty<ProductGenre>();

            return new ProductCardDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name,
                ImageUrl = product.ImageUrl,
                GenreIds = genres.Select(g => g.GenreId).ToArray(),
                GenreNames = genres.Where(g => g.Genre != null).Select(g => g.Genre!.Name).ToArray()
            };
        }

        private async Task<string?> SaveImageAsync(IFormFile? imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return null;
            }

            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath ?? "wwwroot", "images");
            Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return uniqueFileName;
        }
    }
}
