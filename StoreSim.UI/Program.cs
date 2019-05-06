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
                Console.WriteLine("n:\tNew Customer");
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
                                    var orderString = $"{order.Id}: {order.Time}";
                                    Console.WriteLine(orderString);
                                }
                                Console.WriteLine();
                                Console.WriteLine("Select order ID for details or enter \"b\" to go back");
                                Console.WriteLine("To sort order history, select \"c\" (cheapest), \"m\" (most expensive), \"e\" (earliest), or \"l\" (latest)");
                                input = Console.ReadLine();
                                Console.WriteLine();
                                if(int.TryParse(input, out var orderNum) && orderNum > 0 && orderNum <= store.Orders.Count)
                                {
                                    var orderDetail = store.Orders[orderNum-1];
                                    var customer = storeRepository.GetCustomerById(orderDetail.Id);
                                    Console.WriteLine($"OrderId: {orderDetail.Id}");
                                    Console.WriteLine($"Customer: {customer.FirstName} {customer.LastName}");
                                    Console.WriteLine($"Store Location: {store.Address}, {store.City}, {store.State}, {store.Country}");
                                    Console.WriteLine($"Number of Items: {orderDetail.Quantity}");
                                    Console.WriteLine($"Time of Order: {orderDetail.Time}");
                                    Console.WriteLine($"Total Price: {orderDetail.Total}");
                                    Console.WriteLine();
                                    Console.WriteLine("Press any key to go back");
                                    Console.ReadLine();
                                }
                                else if(input == "c")
                                {
                                    sort = "cheapest";
                                    storeRepository.SortOrderHistoryByCheapest(store.Id);
                                }
                                else if(input == "m")
                                {
                                    sort = "most expensive";
                                    storeRepository.SortOrderHistoryByMostExpensive(store.Id);
                                }
                                else if(input == "e")
                                {
                                    sort = "earliest";
                                    storeRepository.SortOrderHistoryByEarliest(store.Id);
                                }
                                else if(input == "l")
                                {
                                    sort = "latest";
                                    storeRepository.SortOrderHistoryByLatest(store.Id);
                                }
                                else if(input == "b")
                                {
                                    break;
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
                        Console.Write("Enter the name of the customer: ");
                        input = Console.ReadLine();
                        var space = input.IndexOf(" ");
                        var firstName = input.Substring(0, space);
                        var lastName = input.Substring(space+1);
                        var index = -1;
                        for(int i=0; i<customers.Count; i++)
                        {
                            if(customers[i].FirstName == firstName && customers[i].LastName == lastName)
                            {
                                var customer = customers[i];
                                while(true)
                                {
                                    Console.WriteLine($"{customer.FirstName} {customer.LastName} has ID {customer.Id}");
                                    Console.WriteLine();
                                    Console.Write("Would you like to place an order?(y/n): ");
                                    input = Console.ReadLine();
                                }
                            }
                        }
                        if(index < 0)
                        {
                            Console.WriteLine("No customers by that name");
                            Console.WriteLine();
                        }
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
