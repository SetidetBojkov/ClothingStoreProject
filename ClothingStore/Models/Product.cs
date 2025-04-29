using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingStore.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [Precision(18, 2)] // добавено
        public decimal Price { get; set; }


        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
