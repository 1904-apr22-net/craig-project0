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
            Country = storeLocation.Country
        };

        public static IEnumerable<Library.Models.Store> Map(IEnumerable<Entities.Location> storeLocations) =>
            storeLocations.Select(Map);
    }
}