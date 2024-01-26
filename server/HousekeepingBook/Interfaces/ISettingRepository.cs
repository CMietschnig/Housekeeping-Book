using HousekeepingBook.Entities;

namespace HousekeepingBook.Interfaces
{
    public interface ISettingRepository
    {
        Settings? GetSettingsById(int id);
        bool UpdateSettingsById(Settings model);
    }
}
