using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestTrash.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ZipCode { get; set; }
        public List<string> TodaysPickups { get; set; }

        [ForeignKey("Test")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser Test { get; set; }

    }
}