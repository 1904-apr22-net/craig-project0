using System;
using System.Collections.Generic;

namespace StoreSim.DataAccess.Entities
{
    public partial class Product
    {
        public Product()
        {
            InventoryItem = new HashSet<InventoryItem>();
            OrderItem = new HashSet<OrderItem>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public virtual ICollection<InventoryItem> InventoryItem { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
