using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreSim.Library.Models
{
    public class Order
    {
        public int Id{ get; set; }
        public int CustomerId{ get; set; }
        public int LocationId{ get; set; }
        public DateTime Time{ get; set; } = new DateTime();
        public int Quantity{ get; set; }
        public decimal Total{ get; set; }

        public List<Product> Products{ get; set; } = new List<Product>();

    }
}