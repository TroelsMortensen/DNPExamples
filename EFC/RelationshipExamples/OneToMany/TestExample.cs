using System;
using System.Collections.Generic;
using System.Linq;

namespace RelationshipExamples.OneToMany
{
    public class TestExample
    {
        public void RunMe()
        {
            using (OneToManyContext ctx = new OneToManyContext())
            {
                IQueryable<Author> queryable = ctx.Authors.Where(author => author.Bio.Contains("Hello"));
                IQueryable<Author> authors = queryable.Where(author => author.LastName.StartsWith("W"));
                List<Author> list = authors.ToList();

                List<Author> authors1 = ctx.Authors.ToList();
                IEnumerable<Author> enumerable = authors1.Where(author => author.Bio.Contains("Hello"));
                
            }
        }
    }

    public class SubCourse
    {
        public string FirstName { get; set; }
        public string EfterNavn { get; set; }
    }
}