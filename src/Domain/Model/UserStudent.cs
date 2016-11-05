using System.Collections.Generic;

namespace Domain.Model
{
    public class UserStudent
    {
        public UserStudent()
        {
            Parents = new List<ChildParent>();
            TestResults = new List<TestResult>();
        }

        public string Id { get; set; }
        public User User { get; set; }

        public Group Group { get; set; }
        public int GroupId { get; set; }

        public List<ChildParent> Parents { get; set; }
        public List<TestResult> TestResults { get; set; }
    }
}