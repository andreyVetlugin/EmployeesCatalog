using AutoMapper;
using EmployeesCatalog.Dal.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Profile = EmployeesCatalog.Dal.DbEntities.Profile;

namespace EmployeesCatalog.Web.Views
{
    public class EmployeeProfileShortView
    {
        public EmployeeShortView Employee { get; set; }
        public ProfileShortView Profile { get; set; }
        //public Profile ToProfileWithEmployee()
        //{
        //    var mapConfig = new MapperConfiguration(cfg =>
        //    {
        //        cfg.CreateMap<EmployeeProfileShortView, Employee>();
        //        cfg.CreateMap<ProfileShortView, Profile>();
        //    });

        //    var mapper = new Mapper(mapConfig);

        //    var value = mapper.Map<Profile>(this);
        //    value.Employee = mapper.Map<Employee>(Employee);
        //    return value;
        //}
    }

    public class EmployeeProfileFullView
    {
        public EmployeeFullView Employee { get; set; }
        public ProfileFullView Profile { get; set; }
        public Profile ToProfileWithEmployee()
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeFullView, Employee>();
                cfg.CreateMap<ProfileFullView, Profile>();
            });

            var mapper = new Mapper(mapConfig);

            var value = mapper.Map<Profile>(this.Profile);
            value.Employee = mapper.Map<Employee>(Employee);
            value.EmployeeId = Employee?.Id;
            return value;
        }
    }

    public static class EmployeeConvertExtentions
    {
        public static EmployeeProfileShortView ToEmployeeProfileShortView(this Profile value)
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeShortView>();
                cfg.CreateMap<Profile, ProfileShortView>();
            });

            var mapper = new Mapper(mapConfig);
            return new EmployeeProfileShortView
            {
                Employee = mapper.Map<EmployeeShortView>(value.Employee)
                ,
                Profile = mapper.Map<ProfileShortView>(value)
            };
        }

        public static EmployeeProfileFullView ToEmployeeProfileFullView(this Profile value)
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeFullView>();
                cfg.CreateMap<Profile, ProfileFullView>();
            });

            var mapper = new Mapper(mapConfig);
            return new EmployeeProfileFullView
            {
                Employee = mapper.Map<EmployeeFullView>(value.Employee)
                ,
                Profile = mapper.Map<ProfileFullView>(value)
            };
        }
    }
}
