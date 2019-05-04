using System;
using StoreSim.Library.Models;
using Xunit;

namespace StoreSim.Tests
{
    public class StoreLibraryTests
    {
        private readonly Store _store = new Store();

        [Fact]
        public void CannotSetEmptyStoreName()
        {
            Assert.ThrowsAny<ArgumentException>(() => _store.Name = string.Empty);
        }
    }
}
