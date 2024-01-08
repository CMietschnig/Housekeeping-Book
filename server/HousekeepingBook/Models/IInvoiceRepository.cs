using HousekeepingBook.Entities;

namespace HousekeepingBook.Models
{
    public interface IInvoiceRepository
    {
        Invoice GetInvoiceById(int id);
        IEnumerable<Invoice> GetInvoicesPerMonthlyInvoiceSummaryId(int id);
        Invoice AddInvoiceToMonthAndYear(Invoice model);
        Invoice UpdateInvoiceById(Invoice model);
        Invoice DeleteInvoiceById(DeleteInvoiceByIdModel model);
    }
}
