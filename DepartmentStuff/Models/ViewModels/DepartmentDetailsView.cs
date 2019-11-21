using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentStuff.Models.ViewModels
{
    public class DepartmentDetailsView
    {
		public Department department { get; set; }

        [Display(Name = "Supervisor")]
        public string SuperVisor { get; set; }

    }
}
