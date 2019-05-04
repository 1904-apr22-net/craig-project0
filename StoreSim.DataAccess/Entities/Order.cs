using System;
using System.Collections.Generic;

namespace StoreSim.DataAccess.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public DateTime Time { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
