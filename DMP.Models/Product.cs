using System.ComponentModel.DataAnnotations;

namespace DMP.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Price can not be negative")]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue,ErrorMessage = "Stock can not be negative")]
        public int Stock { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
