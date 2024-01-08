
using HousekeepingBook.Entities;

namespace HousekeepingBook.Models
{
    public class MockInvoiceRepository : IInvoiceRepository
    {

        private List<Invoice> _invoices;

        public MockInvoiceRepository()
        {
            _invoices = new List<Invoice>() {
                new Invoice() { InvoiceId = 1 , Total= 23.34, CreateTimestamp= new DateTime(), UpdateTimestamp = new DateTime(), MonthlyInvoiceSummaryId= 1, Store= null },
                new Invoice() { InvoiceId = 2 , Total= 45.87, CreateTimestamp= new DateTime(), UpdateTimestamp = new DateTime(), MonthlyInvoiceSummaryId= 1, Store= null },
                new Invoice() { InvoiceId = 3 , Total= 934.87, CreateTimestamp= new DateTime(), UpdateTimestamp = new DateTime(), MonthlyInvoiceSummaryId= 1, Store= null }
            }; 
        }
        

        public Invoice AddInvoiceToMonthAndYear(Invoice model)
        {
            throw new NotImplementedException();
        }

        public Invoice DeleteInvoiceById(DeleteInvoiceByIdModel model)
        {
                var invoice = _invoices.FirstOrDefault(i => i.InvoiceId == model.Id);
                if (invoice != null)
                {
                    _invoices.Remove(invoice);
                }
            
            return invoice!;
        }

        public Invoice GetInvoiceById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Invoice> GetInvoicesPerMonthlyInvoiceSummaryId(int id)
        {
            throw new NotImplementedException();
        }

        public Invoice UpdateInvoiceById(Invoice model)
        {
            throw new NotImplementedException();
        }
    }
}
