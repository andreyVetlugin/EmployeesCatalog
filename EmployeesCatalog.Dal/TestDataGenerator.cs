using EmployeesCatalog.Dal.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeesCatalog.Dal
{
    public static class TestDataGenerator
    {
        public static void GenerateTestData(EmployeeCatalogDbContext dbContext)
        {

            dbContext.Employees.AddRange(
                new Employee
                {
                    Position = "Гробовщик",
                    Subdivision = "кладбищенский",
                    Salary = 123
                }, new Employee
                {
                    Position = "Старший гробовщик",
                    Subdivision = "корпоративный",
                    Salary = 111
                });

            dbContext.Profiles.AddRange(
                new Profile
                {
                    Birthdate = new DateTime(1998, 6, 30),
                    Email = "RepeatYourself@gmail.com",
                    FullName = "Юрий Николаевич Обухов",
                    PhoneNumber = "878976786",
                    Employee = new Employee
                    {
                        Position = "Ломатель баз данных",
                        Subdivision = "Тестеры",
                        Salary = 199999
                    }
                }, new Profile
                {
                    Birthdate = new DateTime(1987, 2, 12),
                    Email = "ILoveCandies@gmail.com",
                    FullName = "Лерой Дженкис",
                    PhoneNumber = "898989898989",Employee = new Employee
                    {
                        Position = "Уборщик",
                        Subdivision = "Чисто",
                        Salary = 200000
                    }
                }, new Profile
                {
                    Birthdate = new DateTime(1996, 1, 3),
                    Email = "bober911@gmail.com",
                    FullName = "Бьерн Страуструп",
                    PhoneNumber = "67676767676767",Employee= new Employee
                    {
                        Position = "Мухобойка",
                        Subdivision = "полезный",
                        Salary = 1
                    }
                }, new Profile
                {
                    Birthdate = new DateTime(2000, 4, 4),
                    Email = "thinking@gmail.com",
                    FullName = "Юдковский ",
                    PhoneNumber = "878976786"
                }, new Profile
                {
                    Birthdate = new DateTime(1998, 6, 30),
                    Email = "ZeroPoint@gmail.com",
                    FullName = "Лобачевский Дмитрий",
                    PhoneNumber = "35353553533"
                }, new Profile
                {
                    Birthdate = new DateTime(1988, 2, 22),
                    Email = "NooWay@gmail.come",
                    FullName = "Василиса Прекрасная",
                    PhoneNumber = "878976786"
                });
            dbContext.SaveChanges();
        }
    }
}
