using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RelationshipExamples.ManyToMany
{
    public class MTMExamples
    {
        public async Task RunExample()
        {
            using (ManyToManyContext ctx = new ManyToManyContext())
            {
                // await AddOneStudent(ctx);
                // await AddTwoCourses(ctx);
                // await EnrollSteveInDNP(ctx, "IT-DNP1Y-A20");
                // await EnrollSteveInDNP(ctx, "IT-SDJ2-A20");
                // await WhichCoursesIsSteveEnrolledIn(ctx);
                // await DeleteSteve(ctx);
            }
        }

        private async Task DeleteSteve(ManyToManyContext ctx)
        {
            Student steve = await ctx.Students.FirstAsync(s => s.StudentNum == 123456);
            ctx.Students.Remove(steve);
            await ctx.SaveChangesAsync();
        }

        private async Task WhichCoursesIsSteveEnrolledIn(ManyToManyContext ctx)
        {
            List<Course> courses = await ctx.Students
                .Where(s => s.StudentNum == 123456)
                .SelectMany(student => student.StudentCourses)
                .Select(studentCourse => studentCourse.Course)
                .ToListAsync();

            Console.WriteLine(JsonSerializer.Serialize(courses, new JsonSerializerOptions
            {
                WriteIndented = true
            }));
        }

        private async Task EnrollSteveInDNP(ManyToManyContext ctx, string courseCode)
        {
            Student steve = await ctx.Students.FirstAsync(s => s.StudentNum == 123456);
            Course dnp = await ctx.Courses.FirstAsync(c => c.CourseCode.Equals(courseCode));
            StudentCourse sc = new StudentCourse
            {
                Course = dnp,
                Student = steve
            };
            
            steve.StudentCourses = new List<StudentCourse>();
            steve.StudentCourses.Add(sc);
            ctx.Update(steve);
            await ctx.SaveChangesAsync();
        }

        private async Task AddOneStudent(ManyToManyContext ctx)
        {
            Student s = new Student
            {
                FirstName = "Steve",
                LastName = "Doe",
                Email = "123456@via.dk",
                StudentNum = 123456
            };
            await ctx.Students.AddAsync(s);
            await ctx.SaveChangesAsync();
        }

        private async Task AddTwoCourses(ManyToManyContext ctx)
        {
            Course sdj2 = new Course
            {
                Abbreviation = "SDJ2",
                Name = "Software Development with UML and Java 2",
                Semester = 2,
                CourseCode = "IT-SDJ2-A20",
                ECTS = 10
            };
            Course dnp1 = new Course
            {
                Abbreviation = "DNP1",
                Name = "I forgot the actual name",
                Semester = 3,
                CourseCode = "IT-DNP1Y-A20",
                ECTS = 5
            };
            await ctx.Courses.AddAsync(sdj2);
            await ctx.Courses.AddAsync(dnp1);
            await ctx.SaveChangesAsync();
        }
    }
}