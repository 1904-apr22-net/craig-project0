using System;
using System.Collections.Generic;

namespace StoreSim.Library.Models
{
    public class InventoryItem
    {
        public int InventoryItemId{ get; set; }
        public int LocationId{ get; set; }
        public int ProductId{ get; set; }
        public int Quantity{ get; set; }
    }
}