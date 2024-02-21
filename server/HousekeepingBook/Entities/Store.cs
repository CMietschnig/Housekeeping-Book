using System.ComponentModel.DataAnnotations;

namespace HousekeepingBook.Entities
{
    public class Store
    {
        [Key]
        public int StoreId { get; set; }

        [Required]
        public string StoreName { get; set; } = string.Empty;

        public DateTime CreateTimestamp { get; set; }

        public DateTime UpdateTimestamp { get; set; }
    }
}
