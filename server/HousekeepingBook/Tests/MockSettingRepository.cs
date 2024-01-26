using HousekeepingBook.Entities;
using HousekeepingBook.Entities.Enums;
using HousekeepingBook.Interfaces;

namespace HousekeepingBook.Tests
{
    public class MockSettingRepository : ISettingRepository
    {

        private List<Settings> _settings;

        public MockSettingRepository() 
        { 
            _settings = new List<Settings>()
            {
                new Settings() { SettingsId = 1, ContributionMembersCount = 2, Year = "2024", MonthId = Month.January, CreateTimestamp= new DateTime(), UpdateTimestamp = new DateTime() },
                new Settings() { SettingsId = 2, ContributionMembersCount = 4, Year = "2024", MonthId = Month.February, CreateTimestamp= new DateTime(), UpdateTimestamp = new DateTime() },
                new Settings() { SettingsId = 3, ContributionMembersCount = 5, Year = "2024", MonthId = Month.April, CreateTimestamp= new DateTime(), UpdateTimestamp = new DateTime() },
            };
        }

        public Settings? GetSettingsById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateSettingsById(Settings model)
        {
            throw new NotImplementedException();
        }
    }
}
