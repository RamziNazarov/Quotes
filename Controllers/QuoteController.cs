using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    }
}