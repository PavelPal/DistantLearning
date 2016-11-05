using System.Collections.Generic;

namespace Domain.Model
{
    public class User
    {
        public User()
        {
            UserSettings = new List<UserSetting>();
            Comments = new List<Comment>();
            Roles = new List<UserRole>();
            Marks = new List<UserMark>();
            IsLocked = true;
            IsEmailConfirmed = false;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool IsLocked { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoType { get; set; }

        public UserTeacher Teacher { get; set; }
        public UserStudent Student { get; set; }
        public UserParent Parent { get; set; }

        public List<UserSetting> UserSettings { get; set; }
        public List<Comment> Comments { get; set; }
        public List<UserRole> Roles { get; set; }
        public List<UserMark> Marks { get; set; }
    }
}