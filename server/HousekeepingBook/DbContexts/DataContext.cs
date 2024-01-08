using HousekeepingBook.Entities;
using Microsoft.EntityFrameworkCore;

namespace HousekeepingBook.DbContexts
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }
        
        public DbSet<Invoice> Invoices =>Set<Invoice>();
        public DbSet<Store> Stores => Set<Store>();
        public DbSet<MonthlyInvoiceSummary> MonthlyInvoiceSummaries => Set<MonthlyInvoiceSummary>();
    }
}
