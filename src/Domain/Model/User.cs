using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Domain.Model
{
    public class User : IdentityUser
    {
        public User()
        {
            UserSettings = new List<UserSetting>();
            Comments = new List<Comment>();
            Marks = new List<UserMark>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoType { get; set; }

        public UserTeacher Teacher { get; set; }
        public UserStudent Student { get; set; }
        public UserParent Parent { get; set; }

        public List<UserSetting> UserSettings { get; set; }
        public List<Comment> Comments { get; set; }
        public List<UserMark> Marks { get; set; }
    }
}