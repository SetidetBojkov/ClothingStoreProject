using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models
{
    public enum OrderStatus
    {
        Pending,
        Shipped,
        Completed
    }

    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
    }
}
