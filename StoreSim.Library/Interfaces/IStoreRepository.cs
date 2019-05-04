using StoreSim.Library.Models;
using System;
using System.Collections.Generic;

namespace StoreSim.Library.Interfaces
{
    public interface IStoreRepository : IDisposable
    {
        IEnumerable<Store> GetStores(string search = null);
    }
}
