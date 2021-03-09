using EmployeesCatalog.Dal.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesCatalog.Web.Views
{
    public class ProfileShortView
    {
        public Guid? Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string FullName { get; set; }
    }
    public class ProfileFullView : ProfileShortView
    {
        [DateRange(18, 65)]
        [Required]
        public DateTime Birthdate { get; set; }
        [EmailAddress]
        [MaxLength(50)]
        [Required]
        public string Email { get; set; }
        [Phone]
        [MaxLength(15)]
        [Required]
        public string PhoneNumber { get; set; }
    }
}
