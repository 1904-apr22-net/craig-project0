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
            XmlSerializer serializer = Dependencies.CreateXmlSerializer();

            using(IStoreRepository storeRepository = Dependencies.CreateStoreRepository())
            {
                RunUi(storeRepository, serializer);
            }
        }

        public static void RunUi(IStoreRepository storeRepository, XmlSerializer serializer)
        {
            bool finished = false;

            Console.Clear();
            Console.WriteLine("\nCraigo's Order Placement Application!\n");

            while(!finished)
            {
                Console.WriteLine("Please select from the menu below, or enter 'q' to quit.\n");
                Console.WriteLine("a. Place and order");
                Console.WriteLine("b. Search a customer");
                Console.WriteLine("c. Search a store location");

                var choice = (char)Console.Read();

                switch(choice)
                {
                    case 'a': Console.WriteLine("Nothing yet"); break;
                    case 'b': Console.WriteLine("Nothing yet"); break;
                    case 'c': Console.WriteLine("Nothing yet"); break;
                    case 'q': finished = true; break;
                    default : Console.WriteLine("\nError: Invalid input\n"); break;
                }
                Console.ReadLine();
            }
        }
    }
}
