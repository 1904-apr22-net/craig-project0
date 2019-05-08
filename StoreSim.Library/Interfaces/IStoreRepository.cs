using StoreSim.Library.Models;
using System;
using System.Collections.Generic;

namespace StoreSim.Library.Interfaces
{
    public interface IStoreRepository : IDisposable
    {
        IEnumerable<Store> GetStores();
        IEnumerable<Customer> GetCustomers();
        IEnumerable<Product> GetProducts();
        Customer GetCustomerById(int id);
        Store GetStoreById(int id);
        Order GetOrderById(int id);
        IEnumerable<Order> GetCustomerOrderHistory(int id);
        IEnumerable<Order> SortOrderHistoryByCheapest(int id);
        IEnumerable<Order> SortOrderHistoryByMostExpensive(int id);
        IEnumerable<Order> SortOrderHistoryByEarliest(int id);
        IEnumerable<Order> SortOrderHistoryByLatest(int id);
        void PlaceOrder(Order order, List<Product> cart);
    }
}
