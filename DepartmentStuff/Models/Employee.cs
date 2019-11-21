using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DepartmentStuff.Models
{
    public class Employee
    {

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public bool IsSuperVisor { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public Department department { get; set; }

        



    }
}