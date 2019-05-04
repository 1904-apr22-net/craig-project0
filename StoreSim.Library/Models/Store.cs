﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreSim.Library.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Address{ get; set; }
        public string City{ get; set; }
        public string State{ get; set; }
        public string Country{ get; set; }
    }
}
