using HousekeepingBook.Entities;
using HousekeepingBook.Models;

namespace HousekeepingBook.Interfaces
{
    public interface IInvoiceRepository
    {
        Invoice? GetInvoiceById(int id);
        IEnumerable<Invoice> GetInvoicesPerMonthlyInvoiceSummaryId(int id);
        bool AddInvoiceToMonthAndYear(Invoice model);
        bool UpdateInvoiceById(Invoice model);
        bool DeleteInvoiceById(DeleteInvoiceByIdModel model);
    }
}
