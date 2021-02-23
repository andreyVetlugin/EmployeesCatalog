using EmployeesCatalog.Dal.ResultTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeesCatalog.Core.RequestHandlers
{
    public interface IRequestHandler<T, TKey> where T : class
    {
        OperationResult Add(T item);
        OperationResult<(int itemsCount, IEnumerable<T> items)> GetRange(int startIndex, int itemsCount, Func<T, bool> predicate = null);
        OperationResult<T> Get(TKey key);
        OperationResult Change(TKey key, T newValue);
        OperationResult Delete(TKey key);
    }
}
