using System.Threading.Tasks;

namespace RelationshipExamples.ManyToMany
{
    public class MTMExamples
    {
        public async Task RunExample()
        {
            using (ManyToManyContext ctx = new ManyToManyContext())
            {
                await AddOneCourse(ctx);
            }
        }

        private async Task AddOneCourse(ManyToManyContext ctx)
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
            ctx.Courses.Add(sdj2);
            ctx.Courses.Add(dnp1);
            await ctx.SaveChangesAsync();
        }
    }
}