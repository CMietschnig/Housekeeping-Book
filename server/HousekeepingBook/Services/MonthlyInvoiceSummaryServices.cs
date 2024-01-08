using HousekeepingBook.DbContexts;
using HousekeepingBook.Entities;
using HousekeepingBook.Entities.Enums;

namespace HousekeepingBook.Services
{
    public class MonthlyInvoiceSummaryServices
    {
        private readonly DataContext context;
        public MonthlyInvoiceSummaryServices(DataContext context)
        {
            this.context = context;
        }

        public int GetMonthlyInvoiceSummaryId(int month, string year)
        {

            var MonthInEnum = (Month)Enum.Parse(typeof(Month), month.ToString());
            var monthlyInvoiceSummary = context.MonthlyInvoiceSummaries.FirstOrDefault(m => m.MonthId == MonthInEnum && m.Year == year);

            if(monthlyInvoiceSummary == null)
            {
                AddNewMonthlyInvoiceSummary(month, year);
                monthlyInvoiceSummary = context.MonthlyInvoiceSummaries.FirstOrDefault(m => m.MonthId == MonthInEnum && m.Year == year);
            }

            return monthlyInvoiceSummary!.MonthlyInvoiceSummaryId;
        }

        public string AddNewMonthlyInvoiceSummary(int month, string year)
        {
            MonthlyInvoiceSummary monthlyInvoiceSummary = new MonthlyInvoiceSummary()
            {
               MonthId = (Month)Enum.Parse(typeof(Month), month.ToString()),
               Year = year,
               Comment = null,
               CreateTimestamp = DateTime.Now,
               UpdateTimestamp = DateTime.Now,
               //Invoices = null,
            };

            context.MonthlyInvoiceSummaries.Add(monthlyInvoiceSummary);
            context.SaveChanges();

            return "MonthlyInvoiceSummary created for " + month + " " + year;
        }

        public MonthlyInvoiceSummary UpdateComment(int id, string comment)
        {
            var monthlyInvoiceSummary = context.MonthlyInvoiceSummaries.FirstOrDefault(m => m.MonthlyInvoiceSummaryId == id);
            if (monthlyInvoiceSummary != null) {
                monthlyInvoiceSummary.Comment = comment;
                context.SaveChanges();
            }
            return monthlyInvoiceSummary;
        }

        public string GetCommentByMonthlyInvoiceSummaryId(int id)
        {
            var monthlyInvoiceSummary = context.MonthlyInvoiceSummaries.FirstOrDefault(c => c.MonthlyInvoiceSummaryId == id);
            return monthlyInvoiceSummary != null ? monthlyInvoiceSummary.Comment : " ";
        }
    }
}
