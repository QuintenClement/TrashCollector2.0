using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTrash.Models
{
    public class ViewModel
    {
        public List<Customer> customers { get; set; }
        public Employee employee { get; set; }
        public string DaySelection { get; set; }
    }
}