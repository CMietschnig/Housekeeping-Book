using HousekeepingBook.DbContexts;
using HousekeepingBook.Entities;
using HousekeepingBook.Interfaces;

namespace HousekeepingBook.Repositories
{
    public class SQLSettingRepository : ISettingRepository
    {
        private readonly DataContext context;

        public SQLSettingRepository(DataContext context)
        {
            this.context = context;
        }

        public Settings? GetSettingsById(int id)
        {
            var settings = context.Settings.Find(id);
            return settings;
        }

        public bool UpdateSettingsById(Settings model)
        {
            var settings = context.Settings.Find(model.SettingsId);
            int affectedRows = 0;
            if (settings != null && model.ContributionMembersCount != 0)
            {
                settings.ContributionMembersCount = model.ContributionMembersCount;
                settings.Year = model.Year;
                settings.MonthId = model.MonthId;
                settings.UpdateTimestamp = model.UpdateTimestamp;
                affectedRows = context.SaveChanges();
            }
            return affectedRows > 0; // Returns true if at least one row was affected.
        }
    }
}
