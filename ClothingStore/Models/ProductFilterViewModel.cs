using System.Collections.Generic;

namespace ClothingStore.Models
{
    public class ProductFilterViewModel
    {
        public List<Product> Products { get; set; } = new();

        public List<Category> Categories { get; set; } = new();

        public int? SelectedCategoryId { get; set; }

        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
    }
}