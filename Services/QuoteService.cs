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
            var response = new GenericResponse<List<QuoteResponse>>();
            try
            {
                response.Data = await _context.Quotes.Include(x => x.Category).Select(x => new QuoteResponse
                        {Id = x.Id, Author = x.Author, Category = x.Category.CategoryName, Quote = x.QuoteText})
                    .ToListAsync();
                response.Succeeded = true;
            }
            catch (Exception e)
            {
                response.Succeeded = false;
                response.Message = "Ошибка сервера";
            }

            return response;
        }
    }
}