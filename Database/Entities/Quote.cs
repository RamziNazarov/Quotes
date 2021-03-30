using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quotes.Database.Entities
{
    public class Quote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string QuoteText { get; set; }

        public DateTime CreateAt { get; set; }
        
        public virtual QuoteCategory Category { get; set; }
    }
}