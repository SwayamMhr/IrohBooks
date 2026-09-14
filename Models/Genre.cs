using System.Text.Json.Serialization;

namespace IrohBooks.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<ProductGenre>? ProductGenres { get; set; }
    }
}
