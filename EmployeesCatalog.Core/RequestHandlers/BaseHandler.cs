using EmployeesCatalog.Dal;
using EmployeesCatalog.Dal.DbEntities;
using EmployeesCatalog.Dal.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeesCatalog.Core.RequestHandlers
{
    public class BaseHandler<T, TKey> : IRequestHandler<T, TKey> where T : class, IDbEntity
    {
        private EmployeeCatalogDbContext dbContext;
        private string typeName;
        public BaseHandler(EmployeeCatalogDbContext dbContext, string typeName)
        {
            this.dbContext = dbContext;
            this.typeName = typeName;
        }

        public OperationResult Add(T value)
        {//verification? 
            dbContext.Add(value);
            dbContext.SaveChanges();
            return OperationResult.BuildSuccess();
        }
        public OperationResult Change(TKey key, T newValue)
        {
            var valueFromBd = dbContext.Find<T>(key);
            if (valueFromBd == null)
                return OperationResult.BuildNotFoundError(typeName + " с таким id не найден");

            //var verificationResult =            //if (!verificationResult.Ok)
            //    return verificationResult;

            dbContext.Entry(valueFromBd).CurrentValues.SetValues(newValue);
            dbContext.SaveChanges();
            return OperationResult.BuildSuccess();
        }

        public OperationResult Delete(TKey key)
        {
            var valueFromBd = dbContext.Find<T>(key);
            if (valueFromBd == null)
                return OperationResult.BuildNotFoundError(typeName + " с таким id не найден");
            dbContext.Remove(valueFromBd);
            dbContext.SaveChanges();
            return OperationResult.BuildSuccess();
        }

        public OperationResult<T> Get(TKey key)
        {
            var valueFromBd = dbContext.Find<T>(key);
            if (valueFromBd == null)
                return OperationResult<T>.BuildNotFoundError(typeName + " с таким id не найден");
            return OperationResult<T>.BuildSuccess(valueFromBd);
        }

        public OperationResult<(int itemsCount, IEnumerable<T> items)> GetRange(int startIndex, int itemsCount, Func<T, bool> predicate = null)
        {
            IEnumerable<T> valuesFromBd;
            if (predicate == null)
                valuesFromBd = dbContext.Set<T>();
            else
                valuesFromBd = dbContext.Set<T>().Where(predicate);
            var count = valuesFromBd.Count();
            var valuesFromBdCuted = valuesFromBd.OrderBy(f => f.Id).Skip(startIndex).Take(itemsCount).ToList();
            return OperationResult<(int itemsCount, IEnumerable<T> items)>.BuildSuccess((count, valuesFromBdCuted));
        }
    }
}
