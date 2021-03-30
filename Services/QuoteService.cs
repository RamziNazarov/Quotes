using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quotes.Common.Wrappers;
using Quotes.Database;
using Quotes.DTOs;
using Quotes.Interfaces.Services;

namespace Quotes.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly ApplicationDbContext _context;

        public QuoteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GenericResponse<List<QuoteResponse>>> GetAllAsync()
        {
            return new GenericResponse<List<QuoteResponse>>
            {
                Data = await _context.Quotes.Include(x => x.Category).Select(x => new QuoteResponse
                        {Id = x.Id, Author = x.Author, Category = x.Category.CategoryName, Quote = x.QuoteText, CreateAt = x.CreateAt})
                    .ToListAsync(),
                Succeeded = true
            };
        }

        public async Task<Response> DeleteByIdAsync(int id)
        {
            var quote = await _context.Quotes.FindAsync(id);
            
            if (quote != null)
            {
                _context.Entry(quote).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return new Response {Message = "Успешно удален", Succeeded = true};
            }

            return new Response {Message = "Quote не найден", Succeeded = false};
        }

        public async Task<Response> UpdateAsync(UpdateQuoteRequest request)
        {
            var quote = await _context.Quotes.FindAsync(request.QuoteId);
            var quoteCategory = await _context.QuoteCategories.FindAsync(request.CategoryId);
            if (quote == null || quoteCategory == null)
            {
                return new Response {Message = "Quote или категория не найдены", Succeeded = false};
            }
            quote.Author = request.Author ?? quote.Author;
            quote.CategoryId = request.CategoryId;
            quote.QuoteText = request.Quote ?? quote.QuoteText;
            _context.Entry(quote).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new Response {Message = "Успешно обновлен", Succeeded = true};
        }

        public async Task<GenericResponse<List<QuoteResponse>>> GetAllByCategoryIdAsync(int id)
        {
            return new GenericResponse<List<QuoteResponse>>
            {
                Data = await _context.Quotes.Where(x => x.CategoryId == id).Include(x => x.Category).Select(x =>
                    new QuoteResponse
                    {
                        Id = x.Id, Author = x.Author, Category = x.Category.CategoryName, Quote = x.QuoteText,
                        CreateAt = x.CreateAt
                    }).ToListAsync(),
                Succeeded = true
            };
        }

        public async Task<GenericResponse<QuoteResponse>> GetRandomQuoteAsync()
        {
            var quotes = await _context.Quotes.Include(x=>x.Category).Select(x=> new QuoteResponse{Author = x.Author, Category = x.Category.CategoryName, Id = x.Id, Quote = x.QuoteText,CreateAt = x.CreateAt}).ToListAsync();
            var quotesIds = quotes.Select(x => x.Id).ToList();
            if (quotesIds.Count > 0)
            {
                int randomNumber = new Random().Next(0, quotesIds.Count - 1);
                return new GenericResponse<QuoteResponse>
                {
                    Data = quotes[randomNumber],
                    Succeeded = true
                };
            }

            return new GenericResponse<QuoteResponse>{Succeeded = true, Message = "Quote-ов нет"};
        }

        public async Task DeleteOldQuotesAsync()
        {
            DateTime currentDateTime = DateTime.Now;
            var quotes = await _context.Quotes.Where(x =>
                x.CreateAt.Hour < currentDateTime.Hour && x.CreateAt.Day <= currentDateTime.Day &&
                x.CreateAt.Year <= currentDateTime.Year).ToListAsync();
            foreach (var quote in quotes)
            {
                _context.Entry(quote).State = EntityState.Deleted;
            }

            await _context.SaveChangesAsync();
        }
    }
}