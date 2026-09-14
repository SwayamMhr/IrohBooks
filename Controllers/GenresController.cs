using IrohBooks.Data;
using IrohBooks.Models;
using IrohBooks.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace IrohBooks.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly Repository<Genre> _genres;

        public GenresController(ApplicationDbContext context)
        {
            _genres = new Repository<Genre>(context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetAll()
        {
            var genres = await _genres.GetAllAsync();
            return Ok(genres.Select(g => new GenreDto { GenreId = g.GenreId, Name = g.Name }));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GenreDto>> GetById(int id)
        {
            var genre = await _genres.GetByIdAsync(id, new QueryOptions<Genre>());
            if (genre == null)
            {
                return NotFound();
            }

            return Ok(new GenreDto { GenreId = genre.GenreId, Name = genre.Name });
        }

        [HttpPost]
        public async Task<ActionResult<GenreDto>> Create(GenreWriteDto dto)
        {
            var genre = new Genre { Name = dto.Name };
            await _genres.AddAsync(genre);
            return CreatedAtAction(nameof(GetById), new { id = genre.GenreId }, new GenreDto
            {
                GenreId = genre.GenreId,
                Name = genre.Name
            });
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GenreDto>> Update(int id, GenreWriteDto dto)
        {
            var existing = await _genres.GetByIdAsync(id, new QueryOptions<Genre>());
            if (existing == null)
            {
                return NotFound();
            }

            existing.Name = dto.Name;
            await _genres.UpdateAsync(existing);
            return Ok(new GenreDto { GenreId = existing.GenreId, Name = existing.Name });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _genres.GetByIdAsync(id, new QueryOptions<Genre>());
            if (existing == null)
            {
                return NotFound();
            }

            await _genres.DeleteAsync(id);
            return NoContent();
        }
    }
}
