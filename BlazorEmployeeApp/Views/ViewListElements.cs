using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesCatalog.Web.Views
{
    public class ViewListElements<T>
    {
        public int ItemsCount { get; set; }
        public List<T> Items { get; set; }
    }
}
