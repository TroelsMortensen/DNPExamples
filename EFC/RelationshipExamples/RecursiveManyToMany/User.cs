using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RelationshipExamples.RecursiveManyToMany
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Name { get; set; }

        public List<UserToUser> FollowsUsers { get; set; }
        
    }
}