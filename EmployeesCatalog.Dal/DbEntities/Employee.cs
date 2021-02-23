using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeesCatalog.Dal.DbEntities
{
    public class Employee : IDbEntity
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Position { get; set; }
        [MaxLength(150)]
        [Required]
        public string Subdivision { get; set; }
        [Range(1, 200000)]
        [Required]
        public double Salary { get; set; }
        //public Guid ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}
