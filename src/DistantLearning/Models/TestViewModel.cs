using System;
using Domain.Model;

namespace DistantLearning.Models
{
    public class TestViewModel
    {
        public TestViewModel(Test test)
        {
            Id = test.Id;
            Name = test.Name;
            IsLocked = test.IsLocked;
            CreatedDate = test.CreatedDate;
            StartedDate = test.StartedDate;
            ClosedDate = test.ClosedDate;
            Teacher = test.Teacher.User;
            Discipline = test.Discipline;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsLocked { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public User Teacher { get; set; }
        public Discipline Discipline { get; set; }
    }

    public class CreateTestViewModel
    {
        public string Name { get; set; }
        public bool IsLocked { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public int DisciplineId { get; set; }
        public CreateTestQuestionViewModel[] Questions { get; set; }
    }

    public class CreateTestQuestionViewModel
    {
        public string Body { get; set; }
        public int Seconds { get; set; }
        public CreateTestAnswerViewModel[] Answers { get; set; }
    }

    public class CreateTestAnswerViewModel
    {
        public string Body { get; set; }
        public bool IsCorrect { get; set; }
    }
}