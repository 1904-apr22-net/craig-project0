using System;
using StoreSim.Library.Interfaces;
using StoreSim.Library.Models;
using StoreSim.DataAccess.Repositories;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Xml.Serialization;

namespace StoreSim.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            using(IStoreRepository storeRepository = Dependencies.CreateStoreRepository())
            {
                RunUi(storeRepository);
            }
        }

        public static void RunUi(IStoreRepository storeRepository)
        {
            Console.Clear();
            Console.WriteLine("Craigo's Skateshop\n");

            while(true)
            {
                Console.WriteLine("Select from the menu below or press \"q\" to quit\n");
                Console.WriteLine("c:\tSearch a customer");
                Console.WriteLine("s:\tSelect a store location");
                Console.WriteLine("p:\tPlace an order");
                var input = Console.ReadLine();
                if(input == "s")
                {
                    var storeLocations = storeRepository.GetStores().ToList();
                    Console.WriteLine();
                    if(storeLocations.Count == 0)
                    {
                        Console.WriteLine("No stores");
                    }
                    while(storeLocations.Count > 0)
                    {
                        for(var i=1; i<=storeLocations.Count; i++)
                        {
                            Store store = storeLocations[i-1];
                            var storeString = $"{i}: {store.Address}, {store.City}, {store.State}, {store.Country}";
                            Console.WriteLine(storeString);
                        }
                        Console.WriteLine();
                        Console.WriteLine("Select a store to see order history or enter \"b\" to go back");
                        input = Console.ReadLine();
                        Console.WriteLine();
                        if(input == "b")
                        {
                            break;
                        }
                        if(int.TryParse(input, out var storeNum) && storeNum > 0 && storeNum <= storeLocations.Count)
                        {
                            Store store = storeLocations[storeNum-1];
                            if(store.Orders.Count == 0)
                            {
                                Console.WriteLine("No order history");
                            }
                            var sort = "cheapest";
                            var sortedOrders = storeRepository.SortOrderHistoryByCheapest(store.Id).ToList();
                            while(sortedOrders.Count > 0)
                            {
                                Console.WriteLine($"Sorting by {sort}:");
                                for(var i=1; i<=sortedOrders.Count; i++)
                                {
                                    var order = sortedOrders[i-1];
                                    var orderString = $"{i}: {order.Time}";
                                    Console.WriteLine(orderString);
                                }
                                Console.WriteLine();
                                Console.WriteLine("Select order ID for details or enter \"b\" to go back");
                                Console.WriteLine("To sort order history, select \"c\" (cheapest), \"m\" (most expensive), \"e\" (earliest), or \"l\" (latest)");
                                input = Console.ReadLine();
                                Console.WriteLine();
                                if(int.TryParse(input, out var orderNum) && orderNum > 0 && orderNum <= store.Orders.Count)
                                {
                                    var orderDetail = sortedOrders[orderNum-1];
                                    var customer = storeRepository.GetCustomerById(orderDetail.CustomerId);
                                    Console.WriteLine($"OrderId: {orderDetail.Id}");
                                    Console.WriteLine($"Customer: {customer.FirstName} {customer.LastName}");
                                    Console.WriteLine($"Store Location: {store.Address}, {store.City}, {store.State}, {store.Country}");
                                    Console.WriteLine($"Number of Items: {orderDetail.Quantity}");
                                    Console.WriteLine($"Time of Order: {orderDetail.Time}");
                                    Console.WriteLine($"Total Price: ${orderDetail.Total.ToString("0.##")}");
                                    Console.WriteLine();
                                    Console.WriteLine("Press enter key to go back");
                                    Console.ReadLine();
                                }
                                else if(input == "c")
                                {
                                    sort = "cheapest";
                                    sortedOrders = storeRepository.SortOrderHistoryByCheapest(store.Id).ToList();
                                }
                                else if(input == "m")
                                {
                                    sort = "most expensive";
                                    sortedOrders = storeRepository.SortOrderHistoryByMostExpensive(store.Id).ToList();
                                }
                                else if(input == "e")
                                {
                                    sort = "earliest";
                                    sortedOrders = storeRepository.SortOrderHistoryByEarliest(store.Id).ToList();
                                }
                                else if(input == "l")
                                {
                                    sort = "latest";
                                    sortedOrders = storeRepository.SortOrderHistoryByLatest(store.Id).ToList();
                                }
                                else if(input == "b")
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Invalid Input");
                                    Console.WriteLine();
                                }
                            }
                        }
                        
                    }
                }
                else if(input == "c")
                {
                    var customers = storeRepository.GetCustomers().ToList();
                    while(true)
                    {
                        Console.WriteLine();
                        Console.Write("Enter the name of the customer or \"b\" to go back: ");
                        input = Console.ReadLine();
                        if(input == "b")
                        {
                            break;
                        }
                        var space = input.IndexOf(" ");
                        var firstName = input.Substring(0, space);
                        var lastName = input.Substring(space+1);
                        var index = -1;
                        for(int i=0; i<customers.Count; i++)
                        {
                            if(customers[i].FirstName == firstName && customers[i].LastName == lastName)
                            {
                                index = i;
                            }
                        }
                        if(index > 0)
                        {
                            var customer = customers[index];
                            while(true)
                            {
                                Console.WriteLine($"Customer ID: {customer.Id}");
                                Console.WriteLine($"First Name: {customer.FirstName}");
                                Console.WriteLine($"Last Name: {customer.LastName}");
                                Console.WriteLine($"Address: {customer.Address}");
                                Console.WriteLine($"City: {customer.City}");
                                Console.WriteLine($"State: {customer.State}");
                                Console.WriteLine($"Country: {customer.Country}");
                                Console.WriteLine();
                                Console.Write("Select \"h\" to view history, or \"b\" to go back");
                                input = Console.ReadLine();
                                if(input == "b")
                                {
                                    break;
                                }
                                else if(input == "h")
                                {
                                    var orderHistory = storeRepository.GetCustomerOrderHistory(customer.Id).ToList();
                                    while(true)
                                    {
                                        for(var i=1; i<=orderHistory.Count; i++)
                                        {
                                            Console.WriteLine($"{orderHistory[i-1].Id}: {orderHistory[i-1].Time}");
                                        }
                                        Console.WriteLine();
                                        Console.WriteLine("Select an order to see details, or \"b\" to go back");
                                        input = Console.ReadLine();
                                        if(input == "b")
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Invalid Input");
                                    Console.WriteLine();
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid Input");
                        }
                    }
                }
                else if(input == "p")
                {
                    Console.WriteLine();
                    Console.Write("Are you an exisiting customer? (y/n): ");
                    input = Console.ReadLine();
                    Console.WriteLine();
                    if(input == "y")
                    {
                        var customers = storeRepository.GetCustomers().ToList();
                        Console.Write("Enter customer name: ");
                        input = Console.ReadLine();
                        Console.WriteLine();
                        var space = input.IndexOf(" ");
                        var firstName = input.Substring(0, space);
                        var lastName = input.Substring(space+1);
                        var index = -1;
                        for(int i=0; i<customers.Count; i++)
                        {
                            if(customers[i].FirstName == firstName && customers[i].LastName == lastName)
                            {
                                index = i;
                            }
                        }
                        if(index > 0)
                        {
                            var customer = customers[index];
                            var newOrder = new Order();
                            var cart = new List<Product>();
                            newOrder.CustomerId = customer.Id;
                            newOrder.Id = 0;
                            var products = storeRepository.GetProducts().ToList();
                            var location = customer.LocationId;
                            while(true)
                            {
                                Console.Clear();
                                Console.WriteLine($"Order Total: ${newOrder.Total}");
                                Console.WriteLine();
                                for(var i=0; i<products.Count; i++)
                                {
                                    Console.WriteLine($"{products[i].Id}: {products[i].Name} ${products[i].Price}");
                                }
                                Console.WriteLine();
                                Console.Write($"Ordering to location: {storeRepository.GetStoreById(location).Address} ");
                                if(location == customer.LocationId)
                                {
                                    Console.Write("(default)");
                                }
                                Console.WriteLine();
                                Console.Write("Select a product to add it to your cart, \"c\" to change order location, \"p\" to place the order, or \"b\" to go back: ");
                                input = Console.ReadLine();
                                if(input == "b")
                                {
                                    break;
                                }
                                else if(input == "p")
                                {
                                    newOrder.Time = DateTime.Now;
                                    newOrder.LocationId = location;
                                    storeRepository.PlaceOrder(newOrder, cart);
                                    Console.Clear();
                                    Console.WriteLine("Order Placed!");
                                    break;
                                }
                                else if(input == "c")
                                {
                                    var stores = storeRepository.GetStores().ToList();
                                    while(true)
                                    {
                                        Console.WriteLine();
                                        for(var i=0; i<stores.Count; i++)
                                        {
                                            Store store = stores[i];
                                            var storeString = $"{store.Id}: {store.Address}, {store.City}, {store.State}, {store.Country}";
                                            Console.WriteLine(storeString);
                                        }
                                        Console.WriteLine();
                                        Console.Write("Select a store ID: ");
                                        input = Console.ReadLine();
                                        if(int.TryParse(input, out var storeId) && storeId > 0 && storeId <= stores.Count)
                                        {
                                            location = storeId;
                                        }
                                        break;
                                    }
                                }
                                else if(int.TryParse(input, out var productNum) && productNum > 0 && productNum <= products.Count)
                                {
                                    var product = products[productNum-1];
                                    newOrder.Products.Add(product);
                                    newOrder.Total += (decimal)product.Price;
                                    newOrder.Quantity++;
                                    cart.Add(product);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Input");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Customer does not exist");
                        }
                    }
                    else if(input == "n")
                    {

                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                        Console.WriteLine();
                    }
                }
                else if(input == "q")
                {
                    break;
                }
            }
        }
    }
}
