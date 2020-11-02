using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace RelationshipExamples.ManyToMany
{
    public class Course
    {
        [Key]
        public string CourseCode { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }
        public int ECTS { get; set; }
        public int Semester { get; set; }
        public IList<StudentCourse> StudentCourses { get; set; }
    }
}