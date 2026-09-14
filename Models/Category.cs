using System.Text.Json.Serialization;

namespace IrohBooks.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Product>? Products { get; set; }
    }
}
