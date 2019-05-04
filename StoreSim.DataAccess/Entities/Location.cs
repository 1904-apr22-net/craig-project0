using System;
using System.Collections.Generic;

namespace StoreSim.DataAccess.Entities
{
    public partial class Location
    {
        public Location()
        {
            Customer = new HashSet<Customer>();
            InventoryItem = new HashSet<InventoryItem>();
            Order = new HashSet<Order>();
        }

        public int LocationId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<InventoryItem> InventoryItem { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
