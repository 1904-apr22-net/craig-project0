using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreSim.DataAccess
{
    public static class Mapper
    {
        public static Library.Models.Store Map(Entities.Location storeLocation) => new Library.Models.Store
        {
            Id = storeLocation.LocationId,
            Address = storeLocation.Address,
            City = storeLocation.City,
            State = storeLocation.State,
            Country = storeLocation.Country,
            Orders = Map(storeLocation.Order).ToList(),
            Customers = Map(storeLocation.Customer).ToList(),
            Inventory = Map(storeLocation.InventoryItem).ToList()
        };

        public static Library.Models.Order Map(Entities.Order order) => new Library.Models.Order
        {
            Id = order.OrderId,
            CustomerId = order.CustomerId,
            LocationId = order.LocationId,
            Time = order.Time,
            Quantity = order.Quantity,
            Total = order.Total,
            Products = Map(order.OrderItem).ToList()
        };

        public static Library.Models.Product Map(Entities.OrderItem orderItem) =>
            Map(orderItem.Product);

        public static Library.Models.Product Map(Entities.Product product) => new Library.Models.Product
        {
            Id = product.ProductId,
            Name = product.Name,
            Price = (double)product.Price
        };

        public static Library.Models.Customer Map(Entities.Customer customer) => new Library.Models.Customer
        {
            Id = customer.CustomerId,
            LocationId = customer.LocationId,
            Address = customer.Address,
            City = customer.City,
            State = customer.State,
            Country = customer.Country,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Orders = Map(customer.Order).ToList()
        };

        public static Library.Models.Product Map(Entities.InventoryItem inventoryItem) => 
            Map(inventoryItem.Product);

        public static IEnumerable<Library.Models.Store> Map(IEnumerable<Entities.Location> storeLocations) =>
            storeLocations.Select(Map);

        public static IEnumerable<Library.Models.Order> Map(IEnumerable<Entities.Order> orders) =>
            orders.Select(Map);

        public static IEnumerable<Library.Models.Customer> Map(IEnumerable<Entities.Customer> customers) =>
            customers.Select(Map);

        public static IEnumerable<Library.Models.Product> Map(IEnumerable<Entities.InventoryItem> inventory) =>
            inventory.Select(Map);

        public static IEnumerable<Library.Models.Product> Map(IEnumerable<Entities.OrderItem> orderItems) =>
            orderItems.Select(Map);
    }
}