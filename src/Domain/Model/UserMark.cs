namespace Domain.Model
{
    public class UserMark
    {
        public User User { get; set; }
        public string UserId { get; set; }
        public Mark Mark { get; set; }
        public int MarkId { get; set; }
    }
}