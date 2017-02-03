using System;
using System.Collections.Generic;
using System.Linq;
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

    public class ShowTestViewModel
    {
        public ShowTestViewModel(Test test)
        {
            Id = test.Id;
            Name = test.Name;
            IsLocked = test.IsLocked;
            CreatedDate = test.CreatedDate;
            StartedDate = test.StartedDate;
            ClosedDate = test.ClosedDate;
            Teacher = test.Teacher.User;
            Discipline = test.Discipline;
            Comments = test.Comments;
            TestResults = test.TestResults;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsLocked { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public User Teacher { get; set; }
        public Discipline Discipline { get; set; }
        public List<Comment> Comments { get; set; }
        public List<TestResult> TestResults { get; set; }
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

    public class GetTestViewModel
    {
        public GetTestViewModel(Test test)
        {
            Id = test.Id;
            Name = test.Name;
            Questions = test.Questions.Select(q => new GetTestQuestionViewModel(q)).ToArray();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public GetTestQuestionViewModel[] Questions { get; set; }
    }

    public class GetTestQuestionViewModel
    {
        public GetTestQuestionViewModel(Question question)
        {
            Id = question.Id;
            Body = question.Body;
            Seconds = question.Seconds;
            Answers = question.Answers.Select(a => new GetTestAnswerViewModel(a)).ToArray();
        }

        public int Id { get; set; }
        public string Body { get; set; }
        public int Seconds { get; set; }
        public GetTestAnswerViewModel[] Answers { get; set; }
    }

    public class GetTestAnswerViewModel
    {
        public GetTestAnswerViewModel(Answer answer)
        {
            Id = answer.Id;
            Body = answer.Body;
        }

        public int Id { get; set; }
        public string Body { get; set; }
        public bool IsChecked { get; set; }
    }

    public class CheckTestViewModel
    {
        public int Id { get; set; }
        public CheckTestQuestionViewModel[] Questions { get; set; }
    }

    public class CheckTestQuestionViewModel
    {
        public int Id { get; set; }
        public CheckTestAnswerViewModel[] Answers { get; set; }
    }

    public class CheckTestAnswerViewModel
    {
        public int Id { get; set; }
        public bool IsChecked { get; set; }
    }

    public class TestResultViewModel
    {
        public TestResultViewModel()
        {
        }

        public TestResultViewModel(string discipline, List<int> points)
        {
            Discipline = discipline;
            Points = points;
            AvrPoint = points.Average();
        }

        public string Discipline { get; set; }
        public double AvrPoint { get; set; }
        public List<int> Points { get; set; }
    }
}