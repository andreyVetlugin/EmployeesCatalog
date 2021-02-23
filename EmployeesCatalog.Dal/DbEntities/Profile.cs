using EmployeesCatalog.Dal.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeesCatalog.Dal.DbEntities
{
    public class Profile: IDbEntity
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        [DateRange(18,65)]
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }
        [Phone]
        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        public Guid? EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
