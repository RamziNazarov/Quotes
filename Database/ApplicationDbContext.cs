using Microsoft.EntityFrameworkCore;
using Quotes.Database.Entities;

namespace Quotes.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<QuoteCategory> QuoteCategories { get; set; }
    }
}