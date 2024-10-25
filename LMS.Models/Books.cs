using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Models
{
    public class Books
    {
        [Key]
        public int Id { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string SerialName { get; set; }
        public bool IsBookAvailable { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
    }
}
