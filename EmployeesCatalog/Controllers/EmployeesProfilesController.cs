using AutoMapper;
using EmployeesCatalog.Core.RequestHandlers;
using EmployeesCatalog.Dal.DbEntities;
using EmployeesCatalog.Web.ApiResults;
using EmployeesCatalog.Web.controllers;
using EmployeesCatalog.Web.Views;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Profile = EmployeesCatalog.Dal.DbEntities.Profile;

namespace EmployeesCatalog.Web.Controllers
{
    public class EmployeesProfilesController : BaseApiController
    {
        private EmployeeProfileHandler requestHandler;
        public EmployeesProfilesController(EmployeeProfileHandler requestHandler)
        {
            this.requestHandler = requestHandler;
        }

        [HttpGet]
        public IActionResult GetEmployeesWithProfiles(int startIndex = 0, int itemsCount = 10)
        {
            var requestResult = requestHandler.GetEmployeesWithProfiles(startIndex, itemsCount);

            return ApiResultGenerator.GenerateResult
                (requestResult.BuildWithChangeData(l => new ViewListElements<EmployeeProfileShortView>
                { Items = l.Item2.Select(a => a.ToEmployeeProfileShortView()).ToList(), ItemsCount = l.itemsCount }));

        }

        [HttpGet("freeEmployees")]
        public IActionResult GetFreeEmployeesList()
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeShortView>());
            var mapper = new Mapper(mapConfig);
            var resultFromHandler = requestHandler.GetFreeEmployees();
            return ApiResultGenerator.GenerateResult(resultFromHandler.BuildWithChangeData(l => l.Select(e=>mapper.Map<EmployeeShortView>(e)).ToList()));
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeWithProfile(Guid id)
        {
            return ApiResultGenerator.GenerateResult
                (requestHandler.GetEmployeeWithProfile(id).BuildWithChangeData(p=>p.ToEmployeeProfileFullView()));
        }

        [HttpPost]
        public IActionResult AddEmployeeWithProfile(EmployeeProfileFullView view)
        {
            return ApiResultGenerator.GenerateResult(requestHandler.AddEmployeeWithProfile(view.ToProfileWithEmployee()));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployeeWithProfile(Guid id)
        {
            return ApiResultGenerator.GenerateResult(requestHandler.DeleteEmployeeWithProfile(id));
        }

        [HttpPut]
        public IActionResult ChangleEmployeeWithProfile(EmployeeProfileFullView view)
        {
            return ApiResultGenerator.GenerateResult(requestHandler.ChangeEmployeeWithProfile(view.ToProfileWithEmployee()));
        }
    }
}
