using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Domain.Model
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoPath { get; set; }

        public List<UserTeacher> Teacher { get; set; }
        public List<UserStudent> Student { get; set; }
        public List<UserParent> Parent { get; set; }
        public List<PendingUserData> PendingUserData { get; set; }
        public List<UserSetting> UserSettings { get; set; }
        public List<Comment> Comments { get; set; }
        public List<UserMark> Marks { get; set; }
    }
}