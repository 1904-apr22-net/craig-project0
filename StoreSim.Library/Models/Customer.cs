using System;
using System.Collections.Generic;

namespace StoreSim.Library.Models
{
    public class Customer
    {
        public int Id{ get; set; }
        public int LocationId{ get; set; }
        public string Address{ get; set; }
        public string City{ get; set; }
        public string State{ get; set; }
        public string Country{ get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Order> Orders{ get; set; } = new List<Order>();
    }
}
