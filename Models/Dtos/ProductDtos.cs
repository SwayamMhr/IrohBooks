using System.ComponentModel.DataAnnotations;

namespace IrohBooks.Models.Dtos
{
    public class ProductCardDto
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public IReadOnlyList<int> GenreIds { get; set; } = Array.Empty<int>();
        public IReadOnlyList<string> GenreNames { get; set; } = Array.Empty<string>();
    }

    public class ProductWriteDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public List<int> GenreIds { get; set; } = new();

        public IFormFile? ImageFile { get; set; }
    }
}
