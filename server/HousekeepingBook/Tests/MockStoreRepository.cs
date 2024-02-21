using HousekeepingBook.Entities;
using HousekeepingBook.Interfaces;

namespace HousekeepingBook.Tests
{
    public class MockStoreRepository : IStoreRepository
    {
        private List<Store> _stores;
        public MockStoreRepository() 
        { 
            _stores = new List<Store>() {
                new Store() { StoreId = 1, StoreName = "Billa", CreateTimestamp= new DateTime(), UpdateTimestamp = new DateTime() },
                new Store() { StoreId = 2, StoreName = "Spar", CreateTimestamp= new DateTime(), UpdateTimestamp = new DateTime() },
                new Store() { StoreId = 3, StoreName = "Billa Plus", CreateTimestamp= new DateTime(), UpdateTimestamp = new DateTime() }
            };
        }

        public Store AddNewStore(Store store)
        {
            throw new NotImplementedException();
        }

        public Store DeleteStoreById(int id)
        {
            throw new NotImplementedException();
        }

        public Store GetStoreById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Store> GetStores()
        {
            throw new NotImplementedException();
        }

        public Store UpdateStoreById(Store store)
        {
            throw new NotImplementedException();
        }
    }
}
