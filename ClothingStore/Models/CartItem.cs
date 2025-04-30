namespace ClothingStore.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }

        // Оригиналният ти модел
        public Product Product { get; set; }

        public int Quantity { get; set; }

        //  Добавени свойства за сериализация и показване в количката:
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
    }
}
