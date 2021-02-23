using AutoMapper;
using EmployeesCatalog.Core.RequestHandlers;
using EmployeesCatalog.Dal.DbEntities;
using EmployeesCatalog.Web.ApiResults;
using EmployeesCatalog.Web.Views;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesCatalog.Web.controllers
{
    public class EmployeesController : BaseApiController
    {
        private IRequestHandler<Employee, Guid> requestHandler;
        public EmployeesController(IRequestHandler<Employee, Guid> handler)
        {
            requestHandler = handler;
        }
        [HttpGet("{id}")]
        public IActionResult GetEmployee(Guid id)
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeFullView>());
            var mapper = new Mapper(mapConfig);
            var resultFromHandler = requestHandler.Get(id);

            return ApiResultGenerator.GenerateResult(resultFromHandler.BuildWithChangeData(f => mapper.Map<EmployeeFullView>(f)));

        }

        [HttpGet]
        public IActionResult GetEmployees(int startIndex = 0, int itemsCount = 10)
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeShortView>());
            var mapper = new Mapper(mapConfig);
            var resultFromHandler = requestHandler.GetRange(startIndex, itemsCount);

            return ApiResultGenerator.GenerateResult
                (resultFromHandler.BuildWithChangeData(l => new ViewListElements<EmployeeShortView>
                { Items = mapper.Map<List<EmployeeShortView>>(l.items), ItemsCount = l.itemsCount }));
        }

        [HttpPost]
        public IActionResult CreateEmployee(EmployeeFullView view)
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeFullView, Employee>());
            var mapper = new Mapper(mapConfig);
            var resultFromHandler = requestHandler.Add(mapper.Map<Employee>(view));

            return ApiResultGenerator.GenerateResult(resultFromHandler);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var resultFromHandler = requestHandler.Delete(id);
            return ApiResultGenerator.GenerateResult(resultFromHandler);
        }

        [HttpPut]
        public IActionResult ChangeEmployee(EmployeeFullView view)
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeFullView, Employee>());
            var mapper = new Mapper(mapConfig);
            var resultFromHandler = requestHandler.Change(view.Id.Value, mapper.Map<Employee>(view)); // проверка на null ? 

            return ApiResultGenerator.GenerateResult(resultFromHandler);
        }
        //public IActionResult;
    }
}
