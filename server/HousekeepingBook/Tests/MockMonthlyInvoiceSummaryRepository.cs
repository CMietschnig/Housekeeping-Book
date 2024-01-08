using HousekeepingBook.Entities;
using HousekeepingBook.Entities.Enums;
using HousekeepingBook.Interfaces;

namespace HousekeepingBook.Tests
{
    public class MockMonthlyInvoiceSummaryRepository : IMonthlyInvoiceSummaryRepository
    {
        private List<MonthlyInvoiceSummary> _monthlyInvoiceSummaries;

        public MockMonthlyInvoiceSummaryRepository()
        {
            _monthlyInvoiceSummaries = new List<MonthlyInvoiceSummary>()
            {
                new MonthlyInvoiceSummary {MonthlyInvoiceSummaryId = 1, MonthId = Month.January, Year = "2024", Comment = null, CreateTimestamp = new DateTime(), UpdateTimestamp = new DateTime()},
                new MonthlyInvoiceSummary {MonthlyInvoiceSummaryId = 2, MonthId = Month.February, Year = "2024", Comment = null, CreateTimestamp = new DateTime(), UpdateTimestamp = new DateTime()},
                new MonthlyInvoiceSummary {MonthlyInvoiceSummaryId = 3, MonthId = Month.March, Year = "2024", Comment = null, CreateTimestamp = new DateTime(), UpdateTimestamp = new DateTime()}
            };
        }

        public string AddNewMonthlyInvoiceSummary(int month, string year)
        {
            throw new NotImplementedException();
        }

        public string GetCommentByMonthlyInvoiceSummaryId(int id)
        {
            throw new NotImplementedException();
        }

        public int GetMonthlyInvoiceSummaryId(int month, string year)
        {
            throw new NotImplementedException();
        }

        public MonthlyInvoiceSummary UpdateComment(int id, string comment)
        {
            throw new NotImplementedException();
        }
    }
}
