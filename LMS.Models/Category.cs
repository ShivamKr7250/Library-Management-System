using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CodeStart { get; set; }
        public string CodeEnd { get; set; }
        public string CategoryName { get; set; }
    }
}
