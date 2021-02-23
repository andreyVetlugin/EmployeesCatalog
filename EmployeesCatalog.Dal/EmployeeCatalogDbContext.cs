using EmployeesCatalog.Dal.DbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeesCatalog.Dal
{
    public class EmployeeCatalogDbContext : DbContext
    {
        public DbSet<Employee> Employees {get;set;}
        public DbSet<Profile> Profiles { get; set;}
        public EmployeeCatalogDbContext(DbContextOptions<EmployeeCatalogDbContext> options):base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
    }
}
