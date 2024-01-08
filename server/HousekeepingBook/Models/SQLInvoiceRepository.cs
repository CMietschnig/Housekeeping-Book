
using HousekeepingBook.DbContexts;
using HousekeepingBook.Entities;

namespace HousekeepingBook.Models
{
    public class SQLInvoiceRepository : IInvoiceRepository
    {
        private readonly DataContext context;

        public SQLInvoiceRepository(DataContext context)
        {
            this.context = context;
        }
        public Invoice AddInvoiceToMonthAndYear(Invoice model)
        {
            context.Invoices.Add(model);
            context.SaveChanges();
            
            return model;
        }

        public Invoice DeleteInvoiceById(DeleteInvoiceByIdModel model)
        {
            var invoice = context.Invoices.Find(model.Id);
            if (invoice != null) {
                context.Invoices.Remove(invoice);
                context.SaveChanges();
            }
            return invoice!;
        }

        public Invoice GetInvoiceById(int id)
        {
            var invoice = context.Invoices.Find(id);
             // check if invoice is null and return invoice or error??
            return invoice;
        }

        public IEnumerable<Invoice> GetInvoicesPerMonthlyInvoiceSummaryId(int id)
        {
            var invoices = context.Invoices.Where(i => i.MonthlyInvoiceSummaryId == id).ToList();
            return invoices;
        }

        public Invoice UpdateInvoiceById(Invoice model)
        {
            var invoice = context.Invoices.Find(model.InvoiceId);
            if (invoice != null)
            {
                invoice.Total = model.Total;
                invoice.UpdateTimestamp = model.UpdateTimestamp;
            }
            //var invoice = context.Invoices.Update(model);
            //invoice.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return invoice;
        }
    }
}
