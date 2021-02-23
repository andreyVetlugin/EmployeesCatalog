using EmployeesCatalog.Dal;
using EmployeesCatalog.Dal.DbEntities;
using EmployeesCatalog.Dal.ResultTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeesCatalog.Core.RequestHandlers
{
    public class EmployeeProfileHandler
    {
        private EmployeeCatalogDbContext dbContext;
        private IRequestHandler<Employee, Guid> employeeHandler;
        private IRequestHandler<Profile, Guid> profileHandler;
        public EmployeeProfileHandler(EmployeeCatalogDbContext dbContext
            , IRequestHandler<Employee, Guid> employeeHandler
            , IRequestHandler<Profile, Guid> profileHandler)
        {
            this.dbContext = dbContext;
            this.employeeHandler = employeeHandler;
            this.profileHandler = profileHandler;
        }

        public OperationResult<(int itemsCount, IEnumerable<Profile>)> GetEmployeesWithProfiles(int startIndex, int itemsCount)
        {
            var valuesFromBd = dbContext.Profiles.OrderBy(f => f.Id).Skip(startIndex).Take(itemsCount).Include(p => p.Employee).ToList();
            return OperationResult<(int itemsCount, IEnumerable<Profile> items)>.BuildSuccess((dbContext.Profiles.Count(), valuesFromBd));
        }
        public OperationResult<IEnumerable<Employee>> GetFreeEmployees()
        {
            return OperationResult<IEnumerable<Employee>>.
                BuildSuccess(dbContext.Employees.Where(e => !dbContext.Profiles.Select(p => p.EmployeeId).Contains(e.Id)).OrderBy(p => p.Id));
        }

        //public OperationResult<Profile> GetEmployeeWithProfile()
        //{ 
        //    var valueFromDb = profileHandler.Get
        //}

        public OperationResult<Profile> GetEmployeeWithProfile(Guid id)
        {
            var profileFromDbResult = profileHandler.Get(id);
            if (!profileFromDbResult.Ok)
                return profileFromDbResult;
            var profile = profileFromDbResult.ResultModel;
            profile.Employee = profile.EmployeeId.HasValue ? dbContext.Employees.Find(profile.EmployeeId) : null;
            return OperationResult<Profile>.BuildSuccess(profile);
        }
        public OperationResult ChangeEmployeeWithProfile(Profile profile)
        {
            var profileFromBdResult = profileHandler.Get(profile.Id);
            if (!profileFromBdResult.Ok)
                return OperationResult.BuildFromOperationResult(profileFromBdResult);
            dbContext.Entry(profileFromBdResult.ResultModel).CurrentValues.SetValues(profile);
            profileFromBdResult.ResultModel.Employee = profile.Employee;

            var attachResult = AttachEmployeerIfNeeded(profileFromBdResult.ResultModel);
            if (!attachResult.Ok)
                return attachResult;

            dbContext.Update(profileFromBdResult.ResultModel);
            dbContext.SaveChanges();
            return OperationResult.BuildSuccess();
        }

        public OperationResult AddEmployeeWithProfile(Profile profile)
        {
            var attachResult = AttachEmployeerIfNeeded(profile);
            if (!attachResult.Ok)
                return attachResult;
            dbContext.Add(profile);
            dbContext.SaveChanges();
            return OperationResult.BuildSuccess();
        }

        public OperationResult DeleteEmployeeWithProfile(Guid EmployeeId)
        {
            var valueFromBd = dbContext.Employees.Find(EmployeeId);
            if (valueFromBd == null)
                return OperationResult.BuildNotFoundError("Сотрудник с таким id не найден");

            var attachedProfile = dbContext.Profiles.FirstOrDefault(p => p.EmployeeId == valueFromBd.Id);
            dbContext.Remove(valueFromBd);
            if (attachedProfile != null)
                dbContext.Remove(attachedProfile);

            dbContext.SaveChanges();
            return OperationResult.BuildSuccess();
        }
        private OperationResult AttachEmployeerIfNeeded(Profile profile)
        {
            if (profile.EmployeeId != null)
            {
                var employeeFromBdResult = employeeHandler.Get(profile.EmployeeId.Value);
                if (!employeeFromBdResult.Ok)
                    return OperationResult.BuildFromOperationResult(employeeFromBdResult);
                dbContext.Entry(employeeFromBdResult.ResultModel).CurrentValues.SetValues(profile.Employee);
                profile.Employee = employeeFromBdResult.ResultModel;
            }
            if (profile.Employee != null)
                dbContext.Attach(profile.Employee);
            return OperationResult.BuildSuccess();
        }
    }
}
