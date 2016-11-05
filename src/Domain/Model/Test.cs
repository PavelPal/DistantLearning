using System;
using System.Collections.Generic;

namespace Domain.Model
{
    public class Test
    {
        public Test()
        {
            Comments = new List<Comment>();
            Questions = new List<Question>();
            TestResults = new List<TestResult>();
            CreatedDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public UserTeacher Teacher { get; set; }
        public int TeacherId { get; set; }
        public Discipline Discipline { get; set; }
        public int DisciplineId { get; set; }
        public bool IsLocked { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? ClosedDate { get; set; }

        public List<Comment> Comments { get; set; }
        public List<Question> Questions { get; set; }
        public List<TestResult> TestResults { get; set; }
    }
}