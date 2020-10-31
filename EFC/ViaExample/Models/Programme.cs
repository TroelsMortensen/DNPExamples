using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ViaExample.Models
{
    public class Programme
    {
        public int Id { get; set; } // defaults to id
        [Required, StringLength(256)] 
        public string Name { get; set; }
        [Required, StringLength(16)] 
        public string ShortName { get; set; }
        [Required, StringLength(4)] 
        public string HeadOfDepartment { get; set; }
        [Required] 
        public string Location { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}