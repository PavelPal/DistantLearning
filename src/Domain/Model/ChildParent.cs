namespace Domain.Model
{
    public class ChildParent
    {
        public UserStudent Student { get; set; }
        public int StudentId { get; set; }
        public UserParent Parent { get; set; }
        public int ParentId { get; set; }
    }
}