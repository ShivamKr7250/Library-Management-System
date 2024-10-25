using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public List<string> Code { get; set; } = new List<string>();
        public string CategoryName { get; set; }
    }
}
