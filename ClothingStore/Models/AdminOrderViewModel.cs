using System;
using System.Collections.Generic;

namespace ClothingStore.Models
{
    public class AdminOrderViewModel
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; }

        public decimal Total { get; set; }

        public List<AdminOrderItemViewModel> Items { get; set; } = new();
    }

    public class AdminOrderItemViewModel
    {
        public string ProductName { get; set; }

        public int Quantity { get; set; }
    }
}
