using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Моля, въведете име на продукта.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Моля, въведете описание.")]
        public string Description { get; set; }

        [Range(0.01, 10000, ErrorMessage = "Цената трябва да е положително число.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Изберете категория.")]
        public int CategoryId { get; set; }

        public string? ImageUrl { get; set; }

        public Category? Category { get; set; }
    }
}
