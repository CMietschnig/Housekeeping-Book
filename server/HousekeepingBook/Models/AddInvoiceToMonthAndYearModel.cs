namespace HousekeepingBook.Models
{
    public class AddInvoiceToMonthAndYearModel
    {
        public int Month { get; set; }
        public string Year { get; set; } = string.Empty;
        public double InvoiceTotal { get; set; }
    }
}
