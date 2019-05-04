using System;
using System.Collections.Generic;

namespace StoreSim.DataAccess.Entities
{
    public partial class InventoryItem
    {
        public int InventoryItemId { get; set; }
        public int ProductId { get; set; }
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }
        public virtual Product Product { get; set; }
    }
}
