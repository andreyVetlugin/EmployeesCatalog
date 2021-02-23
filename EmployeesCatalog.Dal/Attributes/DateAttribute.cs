using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeesCatalog.Dal.Attributes
{
    public class DateRangeAttribute : RangeAttribute
    {
        public DateRangeAttribute(int minYearsOld, int maxYearsOld)
                : base(typeof(DateTime),
            DateTime.Now.AddYears(-maxYearsOld).ToShortDateString(),
            DateTime.Now.AddYears(-minYearsOld).ToShortDateString())
        { }

    }
}
