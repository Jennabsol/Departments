using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DepartmentStuff.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Dept. Name")]
        public string Name { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public int Budget { get; set; }

       public List<Employee> employees { get; set; } = new List<Employee>();
    }
}