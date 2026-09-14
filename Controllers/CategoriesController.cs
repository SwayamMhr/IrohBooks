using IrohBooks.Data;
using IrohBooks.Models;
using IrohBooks.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace IrohBooks.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly Repository<Category> _categories;

        public CategoriesController(ApplicationDbContext context)
        {
            _categories = new Repository<Category>(context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
        {
            var categories = await _categories.GetAllAsync();
            return Ok(categories.Select(c => new CategoryDto { CategoryId = c.CategoryId, Name = c.Name }));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var category = await _categories.GetByIdAsync(id, new QueryOptions<Category>());
            if (category == null)
            {
                return NotFound();
            }

            return Ok(new CategoryDto { CategoryId = category.CategoryId, Name = category.Name });
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create(CategoryWriteDto dto)
        {
            var category = new Category { Name = dto.Name };
            await _categories.AddAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = category.CategoryId }, new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            });
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryDto>> Update(int id, CategoryWriteDto dto)
        {
            var existing = await _categories.GetByIdAsync(id, new QueryOptions<Category>());
            if (existing == null)
            {
                return NotFound();
            }

            existing.Name = dto.Name;
            await _categories.UpdateAsync(existing);
            return Ok(new CategoryDto { CategoryId = existing.CategoryId, Name = existing.Name });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _categories.GetByIdAsync(id, new QueryOptions<Category>());
            if (existing == null)
            {
                return NotFound();
            }

            await _categories.DeleteAsync(id);
            return NoContent();
        }
    }
}
