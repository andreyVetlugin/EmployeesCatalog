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
using Profile = EmployeesCatalog.Dal.DbEntities.Profile;

namespace EmployeesCatalog.Web.controllers
{
    public class ProfilesController : BaseApiController
    {
        private IRequestHandler<Profile, Guid> requestHandler;
        public ProfilesController(IRequestHandler<Profile, Guid> handler)
        {
            requestHandler = handler;
        }
        [HttpGet("{id}")]
        public IActionResult GetProfile(Guid id)
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Profile, ProfileFullView>());
            var mapper = new Mapper(mapConfig);
            var resultFromHandler = requestHandler.Get(id);

            return ApiResultGenerator.GenerateResult(resultFromHandler.BuildWithChangeData(f => mapper.Map<ProfileFullView>(f)));

        }

        [HttpGet]
        public IActionResult GetProfiles(int startIndex = 0, int itemsCount = 10)
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Profile,ProfileFullView>());
            var mapper = new Mapper(mapConfig);
            var resultFromHandler = requestHandler.GetRange(startIndex,itemsCount);

            return ApiResultGenerator.GenerateResult
                (resultFromHandler.BuildWithChangeData(l => new ViewListElements<ProfileFullView>
                { Items = mapper.Map<List<ProfileFullView>>(l.items), ItemsCount = l.itemsCount }));
        }

        [HttpPost]
        public IActionResult CreateProfile(ProfileFullView view)
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<ProfileFullView, Profile>());
            var mapper = new Mapper(mapConfig);
            var resultFromHandler = requestHandler.Add(mapper.Map<Profile>(view));

            return ApiResultGenerator.GenerateResult(resultFromHandler);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProfile(Guid id)
        {
            var resultFromHandler = requestHandler.Delete(id);
            return ApiResultGenerator.GenerateResult(resultFromHandler);
        }

        [HttpPut]
        public IActionResult ChangeProfile(ProfileFullView view)
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<ProfileFullView, Profile>());
            var mapper = new Mapper(mapConfig);
            var resultFromHandler = requestHandler.Change(view.Id.Value, mapper.Map<Profile>(view)); // проверка на null? 

            return ApiResultGenerator.GenerateResult(resultFromHandler);
        }
        //public IActionResult;
    }
}
