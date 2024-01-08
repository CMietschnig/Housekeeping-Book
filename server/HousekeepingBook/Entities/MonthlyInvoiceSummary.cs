﻿using HousekeepingBook.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace HousekeepingBook.Entities
{
    public class MonthlyInvoiceSummary
    {
        [Key]
        public int MonthlyInvoiceSummaryId { get; set; }

        [Required]
        public Month MonthId { get; set; }

        [Required]
        [MaxLength(4)]
        public string Year { get; set; } = string.Empty;

        public string? Comment { get; set; }

        public DateTime CreateTimestamp { get; set; }

        public DateTime UpdateTimestamp { get; set; }

        //[Required]
        //public List<Invoice>? Invoices { get; set; }  // nicht auf der Datenbank????
    }
}