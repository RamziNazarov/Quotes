namespace Quotes.DTOs
{
    public class UpdateQuoteRequest
    {
        public int QuoteId { get; set; }
        public string Author { get; set; }
        public string Quote { get; set; }
        public int CategoryId { get; set; }
    }
}