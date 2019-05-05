using System;
using Microsoft.EntityFrameworkCore;
using StoreSim.Library.Interfaces;
using StoreSim.Library.Models;
using StoreSim.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace StoreSim.DataAccess.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly SkateShopDbContext _dbContext;

        public StoreRepository(SkateShopDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        
        public IEnumerable<Library.Models.Store> GetStores(string search)
        {
            IQueryable<Entities.Location> items = _dbContext.Location
                .Include(i => i.InventoryItem)
                .Include(c => c.Customer);
            if(search != null)
            {
                //  do some search 
            }
            return Mapper.Map(items);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}