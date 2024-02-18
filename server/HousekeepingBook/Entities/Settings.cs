using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HousekeepingBook.Entities
{
    public class Settings
    {
        [Key]
        public int SettingsId { get; set; }

        [Required]
        public int ContributionMembersCount { get; set; }

        [Required]
        [DefaultValue("light")]
        public required string PreferredColorMode { get; set; }

        public DateTime CreateTimestamp { get; set; }

        public DateTime UpdateTimestamp { get; set; }
    }
}
