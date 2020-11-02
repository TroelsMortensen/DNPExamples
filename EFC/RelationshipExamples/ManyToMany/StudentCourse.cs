namespace RelationshipExamples.ManyToMany
{
    public class StudentCourse
    {
        public int StudentNum { get; set; }
        public Student Student { get; set; }
        public string CourseCode { get; set; }
        public Course Course { get; set; }
    }
}