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
                Console.WriteLine("c:\tSelect a customer");
                Console.WriteLine("s:\tSelect a store location");
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
                        if(int.TryParse(input, out var storeNum) && storeNum > 0 && storeNum <= storeLocations.Count)
                        {
                            Store store = storeLocations[storeNum-1];
                            if(store.Orders.Count == 0)
                            {
                                Console.WriteLine("No order history");
                            }
                            while(store.Orders.Count > 0)
                            {
                                for(var i=1; i<=store.Orders.Count; i++)
                                {
                                    var order = store.Orders[i-1];
                                    var orderString = $"{i}: {order.Time}";
                                    Console.WriteLine(orderString);
                                }
                                Console.WriteLine();
                                Console.WriteLine("Select order for details or enter \"b\" to go back");
                                input = Console.ReadLine();
                                break;
                            }
                        }
                        break;
                    }
                }
                break;
            }
        }
    }
}
