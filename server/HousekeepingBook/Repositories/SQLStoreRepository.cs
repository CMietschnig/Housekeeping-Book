using HousekeepingBook.DbContexts;
using HousekeepingBook.Entities;
using HousekeepingBook.Interfaces;

namespace HousekeepingBook.Repositories
{
    public class SQLStoreRepository : IStoreRepository
    {
        private readonly DataContext context;

        public SQLStoreRepository(DataContext context)
        {
            this.context = context;
        }
        public Store AddNewStore(Store store)
        {
            context.Stores.Add(store);
            context.SaveChanges();

            return store;
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
