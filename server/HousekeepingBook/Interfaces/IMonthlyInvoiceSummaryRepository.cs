namespace HousekeepingBook.Interfaces
{
    public interface IMonthlyInvoiceSummaryRepository
    {
        int GetMonthlyInvoiceSummaryId(int month, string year);
        bool AddNewMonthlyInvoiceSummary(int month, string year);
        bool UpdateComment(int id, string comment);
        string? GetCommentByMonthlyInvoiceSummaryId(int id);
    }
}
