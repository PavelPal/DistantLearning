namespace Domain.Model
{
    public class UserSetting
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }
    }
}