using HousekeepingBook.Entities.Enums;

namespace HousekeepingBook.Models
{
    public class UpdateSettingsModel
    {
        public int SettingsId { get; set; }
        public int ContributionMembersCount { get; set; }
        public string Year { get; set; } = string.Empty;
        public Month MonthId { get; set; }
    }
}
