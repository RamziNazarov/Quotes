using System.Collections.Generic;
using System.Threading.Tasks;
using Quotes.Common.Wrappers;
using Quotes.DTOs;

namespace Quotes.Interfaces.Services
{
    public interface IQuoteService
    {
        Task<GenericResponse<List<QuoteResponse>>> GetAllAsync();
        Task<Response> DeleteByIdAsync(int id);
        Task<Response> UpdateAsync(UpdateQuoteRequest request);
        Task<GenericResponse<List<QuoteResponse>>> GetAllByCategoryIdAsync(int id);
        Task<GenericResponse<QuoteResponse>> GetRandomQuoteAsync();
    }
}