using System;
using Microsoft.EntityFrameworkCore;
using Quotes.Database.Entities;

namespace Quotes.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<QuoteCategory> QuoteCategories { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<QuoteCategory>().HasData(
                new QuoteCategory {Id = 1, CategoryName = "FirstCategory"},
                new QuoteCategory {Id = 2, CategoryName = "SecondCategory"},
                new QuoteCategory {Id = 3, CategoryName = "ThirdCategory"}
            );
            builder.Entity<Quote>().HasData(
                new Quote {Id = 1, Author = "Vlad", CategoryId = 1, CreateAt = DateTime.Now, QuoteText = "FirstQuote"},
                new Quote {Id = 2, Author = "Semen", CategoryId = 2, CreateAt = DateTime.Now.AddHours(1), QuoteText = "SecondQuote"},
                new Quote {Id = 3, Author = "Vovan", CategoryId = 3, CreateAt = DateTime.Now.AddHours(2), QuoteText = "ThirdQuote"},
                new Quote {Id = 4, Author = "Ramz", CategoryId = 1, CreateAt = DateTime.Now.AddHours(3), QuoteText = "FourthQuote"},
                new Quote {Id = 5, Author = "Faridun", CategoryId = 2, CreateAt = DateTime.Now.AddHours(4), QuoteText = "FifthQuote"},
                new Quote {Id = 6, Author = "Shukrullo", CategoryId = 3, CreateAt = DateTime.Now.AddHours(5), QuoteText = "SixthQuote"}
            );
        }
    }
}