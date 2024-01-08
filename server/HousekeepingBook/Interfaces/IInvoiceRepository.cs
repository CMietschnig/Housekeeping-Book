using HousekeepingBook.Entities;
using HousekeepingBook.Models;

namespace HousekeepingBook.Interfaces
{
    public interface IInvoiceRepository
    {
        Invoice GetInvoiceById(int id);
        IEnumerable<Invoice> GetInvoicesPerMonthlyInvoiceSummaryId(int id);
        bool AddInvoiceToMonthAndYear(Invoice model);
        Invoice UpdateInvoiceById(Invoice model);
        Invoice DeleteInvoiceById(DeleteInvoiceByIdModel model);
    }
}
