namespace Domain.Model
{
    public class ChildParent
    {
        public UserStudent Student { get; set; }
        public string StudentId { get; set; }
        public UserParent Parent { get; set; }
        public string ParentId { get; set; }
    }
}