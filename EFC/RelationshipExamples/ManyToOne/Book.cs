using System;
using System.ComponentModel.DataAnnotations;

namespace RelationshipExamples.ManyToOne
{
    public class Book
    {
        [Key]
        public string ISBN { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public int NumOfPages { get; set; }
        public Author Author { get; set; }
    }
}