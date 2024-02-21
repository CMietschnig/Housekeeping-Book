using HousekeepingBook.Entities;

namespace HousekeepingBook.Interfaces
{
    public interface IStoreRepository
    {
        Store GetStoreById(int id);
        IEnumerable<Store> GetStores();
        Store AddNewStore(Store store);
        Store UpdateStoreById(Store store);
        Store DeleteStoreById(int id);

    }
}
