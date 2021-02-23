using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesCatalog.Web.Views
{
    public class EmployeeShortView
    {
        public Guid? Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Position { get; set; }
    }

    public class EmployeeFullView : EmployeeShortView
    {
        [MaxLength(150)]
        [Required]
        public string Subdivision { get; set; }
        [Range(1, 200000)]
        [Required]
        public double Salary { get; set; }
    }
}
