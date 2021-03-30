using System;

namespace Quotes.DTOs
{
    public class QuoteResponse
    {
        public string Author { get; set; }
        public string Category { get; set; }
        public string Quote { get; set; }
        public DateTime CreateAt { get; set; }
        public int Id { get; set; }
    }
}