using System.ComponentModel.DataAnnotations;

namespace IrohBooks.Models.Dtos
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class CategoryWriteDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }

    public class GenreDto
    {
        public int GenreId { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class GenreWriteDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
