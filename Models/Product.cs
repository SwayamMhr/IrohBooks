using System.Text.Json.Serialization;

namespace IrohBooks.Models
{
    public class Product
    {
        public Product()
        {
            ProductGenres = new List<ProductGenre>();
        }

        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; } = "https://via.placeholder.com/150";

        [JsonIgnore]
        public Category? Category { get; set; }

        [JsonIgnore]
        public ICollection<OrderItem>? OrderItems { get; set; }

        [JsonIgnore]
        public ICollection<ProductGenre>? ProductGenres { get; set; }
    }
}
