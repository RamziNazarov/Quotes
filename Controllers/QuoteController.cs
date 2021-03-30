using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quotes.Common.Wrappers;
using Quotes.DTOs;
using Quotes.Interfaces.Services;

namespace Quotes.Controllers
{
    public class QuoteController : BaseController
    {
        private readonly IQuoteService _quoteService;

        public QuoteController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var response = await _quoteService.GetAllAsync();
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<JsonResult> Delete([FromBody]DeleteQuoteRequest request)
        {
            var response = await _quoteService.DeleteByIdAsync(request.Id);
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<JsonResult> Update([FromBody]UpdateQuoteRequest request)
        {
            var response = await _quoteService.UpdateAsync(request);
            return new JsonResult(response);
        }

        [HttpGet]
        public async Task<JsonResult> GetAllByCategoryId(int id)
        {
            var response = await _quoteService.GetAllByCategoryIdAsync(id);
            return new JsonResult(response);
        }

        [HttpGet]
        public async Task<JsonResult> GetRandomQuote()
        {
            var response = await _quoteService.GetRandomQuoteAsync();
            return new JsonResult(response);
        }
    }
}