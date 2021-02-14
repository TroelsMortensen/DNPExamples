namespace RelationshipExamples.RecursiveManyToMany
{
    public class UserToUser
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public User FromUser { get; set; }
        public User ToUser { get; set; }
    }
}