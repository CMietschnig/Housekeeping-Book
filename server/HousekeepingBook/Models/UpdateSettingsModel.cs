namespace HousekeepingBook.Models
{
    public class UpdateSettingsModel
    {
        public int SettingsId { get; set; }
        public int ContributionMembersCount { get; set; }
        public string PreferredColorMode { get; set; } = string.Empty;
    }
}
