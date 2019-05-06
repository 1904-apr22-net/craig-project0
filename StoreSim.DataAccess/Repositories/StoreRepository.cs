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

        
        public IEnumerable<Library.Models.Store> GetStores()
        {
            IQueryable<Entities.Location> items = _dbContext.Location
                .Include(i => i.InventoryItem)
                    .ThenInclude(p => p.Product)
                .Include(o => o.Order)
                    .ThenInclude(oi => oi.OrderItem)
                        .ThenInclude(p => p.Product)
                .Include(c => c.Customer)
                    .ThenInclude(o => o.Order)
                        .ThenInclude(oi => oi.OrderItem)
                            .ThenInclude(p => p.Product);
            return Mapper.Map(items);
        }
        public IEnumerable<Library.Models.Customer> GetCustomers()
        {
            IQueryable<Entities.Customer> items = _dbContext.Customer
                .Include(o => o.Order)
                    .ThenInclude(oi => oi.OrderItem)
                        .ThenInclude(p => p.Product);
            return Mapper.Map(items);
        }

        public Library.Models.Store GetStoreById(int id) =>
            Mapper.Map(_dbContext.Location.Find(id));
        public Library.Models.Customer GetCustomerById(int id) =>
            Mapper.Map(_dbContext.Customer.Find(id));


        public IEnumerable<Library.Models.Order> SortOrderHistoryByCheapest(int id) =>
            Mapper.Map(_dbContext.Location.Find(id).Order.OrderBy(t => t.Total)).ToList();
        public IEnumerable<Library.Models.Order> SortOrderHistoryByMostExpensive(int id) =>
            Mapper.Map(_dbContext.Location.Find(id).Order.OrderByDescending(t => t.Total));
        public IEnumerable<Library.Models.Order> SortOrderHistoryByEarliest(int id) =>
            Mapper.Map(_dbContext.Location.Find(id).Order.OrderBy(t => t.Time));
        public IEnumerable<Library.Models.Order> SortOrderHistoryByLatest(int id) =>
            Mapper.Map(_dbContext.Location.Find(id).Order.OrderByDescending(t => t.Time));

        public IEnumerable<Library.Models.Order> GetCustomerOrderHistory(int id) =>
            Mapper.Map(_dbContext.Customer.Find(id).Order);

        public void Save()
        {
            _dbContext.SaveChanges();
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