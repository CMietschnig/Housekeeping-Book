using HousekeepingBook.Entities;
using HousekeepingBook.Interfaces;
using HousekeepingBook.Models;

namespace HousekeepingBook.Tests
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

        // create a seperate IMonthlyInvoiceSummaryRepository and change the service
        public bool AddInvoiceToMonthAndYear(Invoice model)
        {
            throw new NotImplementedException();
        }

        public bool DeleteInvoiceById(int id)
        {
            throw new NotImplementedException();
        }

        public Invoice? GetInvoiceById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Invoice> GetInvoicesPerMonthlyInvoiceSummaryId(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateInvoiceById(Invoice model)
        {
            throw new NotImplementedException();
        }
    }
}
