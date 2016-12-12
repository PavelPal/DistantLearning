using System.Collections.Generic;

namespace Domain.Model
{
    public class UserStudent
    {
        public int Id { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }
        public Group Group { get; set; }
        public int GroupId { get; set; }

        public List<ChildParent> Parents { get; set; }
        public List<TestResult> TestResults { get; set; }
    }
}