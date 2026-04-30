using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace IrohBooks.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }

        [ValidateNever]
        public ICollection<ProductGenre> ProductGenres { get; set; }
    }
}