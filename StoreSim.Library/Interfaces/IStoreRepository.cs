using StoreSim.Library.Models;
using System;
using System.Collections.Generic;

namespace StoreSim.Library.Interfaces
{
    public interface IStoreRepository : IDisposable
    {
        IEnumerable<Store> GetStores();
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomerById(int id);
        Store GetStoreById(int id);
        IEnumerable<Order> GetCustomerOrderHistory(int id);
        IEnumerable<Order> SortOrderHistoryByCheapest(int id);
        IEnumerable<Order> SortOrderHistoryByMostExpensive(int id);
        IEnumerable<Order> SortOrderHistoryByEarliest(int id);
        IEnumerable<Order> SortOrderHistoryByLatest(int id);

    }
}
