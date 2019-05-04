using Microsoft.EntityFrameworkCore;
using StoreSim.DataAccess.Entities;
using StoreSim.DataAccess.Repositories;
using StoreSim.Library.Interfaces;
using StoreSim.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace StoreSim.UI
{
    public static class Dependencies
    {
        public static IStoreRepository CreateStoreRepository()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SkateShopDbContext>();
            optionsBuilder.UseSqlServer(SecretConfiguration.ConnectionString);

            var dbContext = new SkateShopDbContext(optionsBuilder.Options);

            return new StoreRepository(dbContext);
        }

        public static XmlSerializer CreateXmlSerializer() =>
            new XmlSerializer(typeof(List<Library.Models.Store>));
    }
}