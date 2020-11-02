using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RelationshipExamples.ManyToMany
{
    public class Student
    {
        [Key]
        public int StudentNum { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public IList<StudentCourse> StudentCourses { get; set; }
    }
}