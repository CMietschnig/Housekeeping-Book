using HousekeepingBook.DbContexts;
using HousekeepingBook.Entities;
using HousekeepingBook.Entities.Enums;
using HousekeepingBook.Interfaces;

namespace HousekeepingBook.Repositories
{
    public class SQLMonthlyInvoiceSummaryRepository : IMonthlyInvoiceSummaryRepository
    {
        private readonly DataContext context;

        public SQLMonthlyInvoiceSummaryRepository(DataContext context)
        {
            this.context = context;
        }
        public bool AddNewMonthlyInvoiceSummary(int month, string year)
        {

            // Validate the month
            if (!Enum.IsDefined(typeof(Month), month))
            {
                // Month is not valid
                return false;
            }

            MonthlyInvoiceSummary monthlyInvoiceSummary = new MonthlyInvoiceSummary()
            {
                MonthId = (Month)Enum.Parse(typeof(Month), month.ToString()),
                Year = year,
                Comment = null,
                CreateTimestamp = DateTime.Now,
                UpdateTimestamp = DateTime.Now,
                //Invoices = null,
            };

            int affectedRows = 0;
            IEnumerable <MonthlyInvoiceSummary> summaries = 
                context.MonthlyInvoiceSummaries.Where(
                    x => x.MonthId == monthlyInvoiceSummary.MonthId 
                    && x.Year == monthlyInvoiceSummary.Year);

            if(summaries.Count() == 0)
            {
                context.MonthlyInvoiceSummaries.Add(monthlyInvoiceSummary);
                affectedRows = context.SaveChanges();
            }

            return affectedRows > 0; // Returns true if at least one row was affected.
        }

        public string? GetCommentByMonthlyInvoiceSummaryId(int id)
        {
            var monthlyInvoiceSummary = context.MonthlyInvoiceSummaries.FirstOrDefault(c => c.MonthlyInvoiceSummaryId == id);
            return monthlyInvoiceSummary?.Comment;
        }

        public int GetMonthlyInvoiceSummaryId(int month, string year)
        {
            // Validate the month
            if (!Enum.IsDefined(typeof(Month), month))
            {
                // Month is not valid
                return 0;
            }

            var MonthInEnum = (Month)Enum.Parse(typeof(Month), month.ToString());
            var monthlyInvoiceSummary = context.MonthlyInvoiceSummaries.FirstOrDefault(m => m.MonthId == MonthInEnum && m.Year == year);

            if (monthlyInvoiceSummary == null)
            {
                AddNewMonthlyInvoiceSummary(month, year);
                monthlyInvoiceSummary = context.MonthlyInvoiceSummaries.FirstOrDefault(m => m.MonthId == MonthInEnum && m.Year == year);
            }

            return monthlyInvoiceSummary!.MonthlyInvoiceSummaryId;
        }

        public bool UpdateComment(int id, string comment)
        {
            var monthlyInvoiceSummary = context.MonthlyInvoiceSummaries.FirstOrDefault(m => m.MonthlyInvoiceSummaryId == id);
            int affectedRows = 0;
            if (monthlyInvoiceSummary != null && comment != null)
            {
                monthlyInvoiceSummary.Comment = comment;
                affectedRows = context.SaveChanges();
            }
            return affectedRows > 0; // Returns true if at least one row was affected.
        }
    }
}
