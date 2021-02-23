using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeesCatalog.Dal.DbEntities
{
    public interface IDbEntity
    {
        Guid Id { get; set; }
    }
}
