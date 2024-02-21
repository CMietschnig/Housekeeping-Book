using HousekeepingBook.DbContexts;
using HousekeepingBook.Entities;
using HousekeepingBook.Interfaces;

namespace HousekeepingBook.Repositories
{
    public class SQLInvoiceRepository : IInvoiceRepository
    {
        private readonly DataContext context;

        public SQLInvoiceRepository(DataContext context)
        {
            this.context = context;
        }
        public bool AddInvoiceToMonthAndYear(Invoice model)
        {
            context.Invoices.Add(model);
            int affectedRows = context.SaveChanges();

            return affectedRows > 0; // Returns true if at least one row was affected.
        }

        public bool DeleteInvoiceById(int id)
        {
            var invoice = context.Invoices.Find(id);
            int affectedRows = 0;
            if (invoice != null)
            {
                context.Invoices.Remove(invoice);
                affectedRows = context.SaveChanges();
            }
            return affectedRows > 0; // Returns true if at least one row was affected.
        }

        public Invoice? GetInvoiceById(int id)
        {
            var invoice = context.Invoices.Find(id);
            return invoice;
        }

        public IEnumerable<Invoice> GetInvoicesPerMonthlyInvoiceSummaryId(int id)
        {
            var invoices = context.Invoices.Where(i => i.MonthlyInvoiceSummaryId == id).ToList();
            return invoices;
        }

        public bool UpdateInvoiceById(Invoice model)
        {
            var invoice = context.Invoices.Find(model.InvoiceId);
            int affectedRows = 0;
            if (invoice != null)
            {
                invoice.Total = model.Total;
                invoice.UpdateTimestamp = model.UpdateTimestamp;
                //invoice.Store = model.Store; will be added later!
                affectedRows = context.SaveChanges();
            }
            return affectedRows > 0; // Returns true if at least one row was affected.
        }
    }
}
