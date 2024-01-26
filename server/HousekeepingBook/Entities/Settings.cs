using HousekeepingBook.Entities.Enums;
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
        [MaxLength(4)]
        public string Year { get; set; } = string.Empty;

        [Required]
        public Month MonthId { get; set; }
    }
}
