using EmployeesCatalog.Dal.ResultTypes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeesCatalog.Web.ApiResults
{
    public class ApiResult : IActionResult
    {
        //private readonly JsonResult jsonResult;
        private readonly OperationResult operationResult;

        public ApiResult(OperationResult operationResult)
        {
            this.operationResult = operationResult;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            if (!operationResult.Ok)
            {

                var jsonResult = new JsonResult(operationResult.ErrorMessage, new JsonSerializerOptions()//new JsonSerializerSettings()
                {
                    //ContractResolver = new CamelCasePropertyNamesContractResolver(),
                });

                switch (operationResult.ErrorType)
                {
                    case ErrorType.InvalidForm:
                        {
                            jsonResult.StatusCode = 400;
                            break;
                        }
                    case ErrorType.InvalidInnerState:
                        {
                            jsonResult.StatusCode = 406;
                            break;
                        }
                    case ErrorType.NotFound:
                        {
                            jsonResult.StatusCode = 404;
                            break;
                        }
                    default:
                        throw new NotImplementedException();
                        //return jsonResult.ExecuteResultAsync(context);
                }
                return jsonResult.ExecuteResultAsync(context);
            }
            return new JsonResult(operationResult).ExecuteResultAsync(context);
        }
    }

    public class ApiResult<TModel> : IActionResult
    {
        private readonly OperationResult<TModel> operationResult;

        public ApiResult(OperationResult<TModel> operationResult)
        {
            this.operationResult = operationResult;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            if (!operationResult.Ok)
            {

                var jsonResult = new JsonResult(operationResult.ErrorMessage, new JsonSerializerOptions()
                {

                });

                switch (operationResult.ErrorType)
                {
                    case ErrorType.InvalidForm:
                        {
                            jsonResult.StatusCode = 400;
                            break;
                        }
                    case ErrorType.InvalidInnerState:
                        {
                            jsonResult.StatusCode = 406;
                            break;
                        }
                    default:
                        throw new NotImplementedException();
                }
                return jsonResult.ExecuteResultAsync(context);
            }
            return new JsonResult(operationResult.ResultModel).ExecuteResultAsync(context);
            //return new JsonResult(operationResult).ExecuteResultAsync(context);            
        }
    }
}
