using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace LMS.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string MembershipType { get; set; } // e.g., "Standard", "Premium"
        public DateTime MembershipExpiry { get; set; }
        [NotMapped]
        public string Role {  get; set; }
    }
}
