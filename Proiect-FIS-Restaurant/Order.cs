using System;
using System.Collections.Generic;

namespace Proiect_FIS_Restaurant
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime EstimatedTime { get; set; }
        public string Status { get; set; } // "În așteptare", "Preparare", "Servită"
        public int ReceiptNumber { get; set; }
        public List<OrderItem> Items { get; set; }
    }

    public class OrderItem
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
