using System;

namespace ClothingStore.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Total { get; set; }

        public string Status { get; set; }

        public string CustomerName { get; set; } // използва се в Admin панела
    }
}
