using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ViaExample.DataAccess;
using ViaExample.Models;

namespace ViaExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // await new Program().InsertCourse();
            // await new Program().InsertProgramme();
            // await new Program().UpdateCourse();
            await new Program().SelectExample();
        }

        private async Task SelectExample()
        {
            using (ViaDBContext dbContext = new ViaDBContext())
            {
                var subTypes = dbContext.Courses.Select(c => new
                {
                    Name = c. Name,
                    Semester = c.Semester
                });
                var list = subTypes.ToList();
                foreach (var s in list)
                {
                    Console.WriteLine(s.Name + " " + s.Semester);
                }
            }
        }

        private async Task UpdateCourse()
        {
            using (ViaDBContext dbContext = new ViaDBContext())
            {
                IQueryable<Course> result = dbContext.Courses.Where(c => c.Id.Equals("DNP1"));
                
                Course dnp = await dbContext.Courses.FirstAsync(c => c.Id.Equals("DNP1"));
                Programme softwareProgramme = await dbContext.Programmes.
                    Include(p => p.Courses).
                    FirstAsync(p => p.ShortName.Equals("Software"));
                softwareProgramme.Courses.Add(dnp);
                dbContext.Update(softwareProgramme);
                await dbContext.SaveChangesAsync();
            }
        }

        private async Task InsertProgramme()
        {
            Course SDJ2 = new Course
            {
                Id = "SDJ2",
                Name = "Software Development with Java and UML 2",
                Semester = 2,
                IsElective = false,
                ECTS = 10
            };
            Course GMD = new Course
            {
                Id = "GMD1",
                Name = "Game Development",
                Semester = 6,
                IsElective = true,
                ECTS = 5
            };
            List<Course> courses = new List<Course> {GMD, SDJ2};
            Programme software = new Programme
            {
                Location = "Horsens",
                Name = "Software Technology Engineering",
                ShortName = "Software",
                HeadOfDepartment = "AHM",
                Courses = courses
            };

            using (ViaDBContext dbContext = new ViaDBContext())
            {
                await dbContext.Programmes.AddAsync(software);
                await dbContext.SaveChangesAsync();
            }
        }

        private async Task InsertCourse()
        {
            Course dnp1 = new Course
            {
                Id = "DNP1",
                Name = "Internet Technologies, C# and .NET",
                Semester = 3,
                IsElective = false,
                ECTS = 5
            };

            using (ViaDBContext dbContext = new ViaDBContext())
            {
                await dbContext.Courses.AddAsync(dnp1);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}