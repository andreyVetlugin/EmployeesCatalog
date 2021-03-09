using EmployeesCatalog.Dal.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesCatalog.Web.ApiResults
{
    public class ApiResultGenerator
    {
        public static ApiResult GenerateResult(OperationResult operationResult) => new ApiResult(operationResult);
        public static ApiResult<TModel> GenerateResult<TModel>(OperationResult<TModel> operationResult) => new ApiResult<TModel>(operationResult);
    }
}
