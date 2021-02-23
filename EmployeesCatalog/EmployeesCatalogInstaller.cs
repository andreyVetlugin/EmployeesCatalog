using EmployeesCatalog.Core.RequestHandlers;
using EmployeesCatalog.Dal;
using EmployeesCatalog.Dal.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EmployeesCatalog.Web
{
    public class EmployeesCatalogInstaller
    {
        private readonly IConfiguration configuration;
        public EmployeesCatalogInstaller(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Install(IServiceCollection services)
        {

            var options = new DbContextOptionsBuilder<EmployeeCatalogDbContext>()
                      .UseInMemoryDatabase(databaseName: "EmployeeCatalog")
                      .Options;

            services.AddMvc();
            services.AddDbContext <EmployeeCatalogDbContext>(options=>options.UseInMemoryDatabase(databaseName: "EmployeeCatalog"));
            services.AddScoped<IRequestHandler<Profile, Guid>, BaseHandler<Profile, Guid>>(h => new BaseHandler<Profile, Guid>(new EmployeeCatalogDbContext(options), "Профиль"))
            .AddScoped<IRequestHandler<Employee, Guid>, BaseHandler<Employee, Guid>>(h => new BaseHandler<Employee, Guid>(new EmployeeCatalogDbContext(options), "Сотрудник"))
            .AddScoped<EmployeeProfileHandler>();

            var context = new EmployeeCatalogDbContext(options);
            TestDataGenerator.GenerateTestData(context);

        }
    }
}
