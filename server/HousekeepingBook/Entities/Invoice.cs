using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HousekeepingBook.Entities
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }

        [Required]
        public double Total { get; set; }

        public DateTime CreateTimestamp { get; set; }

        public DateTime UpdateTimestamp { get; set; }

        [Required]
        [ForeignKey("MonthlyInvoiceSummary")]
        public int MonthlyInvoiceSummaryId { get; set; }

        public Store? Store { get; set; }
    }
}
