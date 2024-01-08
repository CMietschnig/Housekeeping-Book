using HousekeepingBook.Entities;

namespace HousekeepingBook.Interfaces
{
    public interface IMonthlyInvoiceSummaryRepository
    {
        int GetMonthlyInvoiceSummaryId(int month, string year);
        string AddNewMonthlyInvoiceSummary(int month, string year);
        MonthlyInvoiceSummary UpdateComment(int id, string comment);
        string GetCommentByMonthlyInvoiceSummaryId(int id);
    }
}
