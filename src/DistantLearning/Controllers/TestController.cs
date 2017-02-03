using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessProvider;
using DistantLearning.Models;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DistantLearning.Controllers
{
    [Authorize]
    [Route("api/test")]
    public class TestController : Controller
    {
        private readonly DomainModelContext _context;

        public TestController(DomainModelContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public async Task<List<TestViewModel>> Tests(string searchString, int skip, int take)
        {
            List<Test> dbTests;
            if (string.IsNullOrEmpty(searchString))
            {
                dbTests = await _context.Tests.Include("Discipline")
                    .Include("Teacher.User")
                    .OrderByDescending(t => t.CreatedDate)
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
            }
            else
            {
                var searchStringToLower = searchString.ToLower();
                dbTests = await _context.Tests.Include("Discipline")
                    .Include("Teacher.User")
                    .Where(t =>
                        t.Name.ToLower().Contains(searchStringToLower) ||
                        searchStringToLower.Contains(t.Name.ToLower()))
                    .OrderByDescending(t => t.CreatedDate)
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
            }
            return dbTests.Select(t => new TestViewModel(t)).ToList();
        }

        [HttpGet("{id}")]
        public async Task<object> Test(int? id)
        {
            if (id == null)
                return "Invalid id";
            var test = await _context.Tests.Include("Questions.Answers").FirstOrDefaultAsync(t => t.Id == id);
            if (test == null)
                return "Not found";
            return test;
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost("create")]
        public string CreateTest([FromBody] CreateTestViewModel test)
        {
            if (test == null)
                return "Invalid data";
            var user =
                _context.Users.Include("Teacher")
                    .FirstOrDefault(u => u.IsApproved && u.UserName.Equals(User.Identity.Name));
            if (user == null)
                return "Invalid user";
            var newTest = new Test
            {
                Name = test.Name,
                IsLocked = test.IsLocked,
                StartedDate = test.StartedDate,
                ClosedDate = test.ClosedDate,
                TeacherId = user.Teacher.FirstOrDefault().Id,
                DisciplineId = test.DisciplineId,
                Questions =
                    test.Questions.Select(
                            q =>
                                new Question
                                {
                                    Body = q.Body,
                                    Seconds = q.Seconds,
                                    Answers =
                                        q.Answers.Select(a => new Answer {Body = a.Body, IsCorrect = a.IsCorrect})
                                            .ToList()
                                })
                        .ToList()
            };
            _context.Tests.Add(newTest);
            _context.SaveChanges();
            return "Created";
        }

        [Authorize(Roles = "Teacher, Admin")]
        [HttpPost("delete/{id}")]
        public async Task<object> DeleteTest(int? id)
        {
            if (id == null)
                return "Invalid id";
            if (User.IsInRole("Admin"))
            {
                var test =
                    await _context.Tests.Include("Comments")
                        .Include("Questions.Answers")
                        .FirstOrDefaultAsync(t => t.Id == id);
                if (test == null)
                    return "Not found";
                _context.Tests.Remove(test);
            }
            else
            {
                var user =
                    await _context.Users.Include("Teacher.Tests.Comments").Include("Teacher.Tests.Questions.Answers")
                        .FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));
                if (user == null)
                    return "User not found";
                if (user.Teacher.FirstOrDefault().Tests.FirstOrDefault(t => t.Id == id) == null)
                    return "Not found";
                _context.Tests.Remove(user.Teacher.FirstOrDefault().Tests.FirstOrDefault(t => t.Id == id));
            }
            _context.ChangeTracker.DetectChanges();
            await _context.SaveChangesAsync();
            return "Deleted";
        }

        [Authorize(Roles = "Student")]
        [HttpGet("get/{id}")]
        public async Task<object> GetTest(int? id)
        {
            if (id == null)
                return "Invalid id";
            var user =
                await _context.Users.Include("Student.TestResults")
                    .FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));
            if (user == null)
                return "User not found";
            var test =
                _context.Tests.Include("TestResults")
                    .Include("Questions.Answers")
                    .FirstOrDefault(
                        t =>
                            !t.IsLocked && t.Id == id.Value &&
                            (t.ClosedDate == null || t.ClosedDate.Value > DateTime.Now) &&
                            (t.StartedDate == null || t.StartedDate.Value < DateTime.Now));
            if (test == null)
                return "Test not found";
            if (test.TestResults.Any(tr => tr.UserId == user.Student.FirstOrDefault().Id))
                return "Completed";
            return new GetTestViewModel(test);
        }

        [HttpGet("testResults/{id}")]
        public async Task<object> GetTestResults(string id)
        {
            if (string.IsNullOrEmpty(id))
                return "Invalid id";
            var user =
                await _context.Users.Include("Student.TestResults")
                    .FirstOrDefaultAsync(u => u.Id.Equals(id));
            if (user == null)
                return "User not found";
            var testResults =
                _context.TestResults
                    .Include("Test.Discipline")
                    .Where(tr => tr.UserId == user.Student.FirstOrDefault().Id)
                    .GroupBy(tr => tr.Test.Discipline)
                    .Select(g => new TestResultViewModel(g.Key.Name, g.Select(
                            innerTr =>
                                Convert.ToInt32(
                                    Math.Round((double) (innerTr.Correct * 10 / (innerTr.Correct + innerTr.Wrong)))))
                        .ToList())).ToList();
            return testResults;
        }

        [Authorize(Roles = "Student")]
        [HttpPost("checkResult")]
        public string CheckResult([FromBody] CheckTestViewModel test)
        {
            if (test == null)
                return "Invalid model";
            var user = _context.Users.Include("Student.TestResults")
                .FirstOrDefault(u => u.UserName.Equals(User.Identity.Name));
            if (user == null)
                return "User not found";
            var testDb =
                _context.Tests.Include("TestResults")
                    .Include("Questions.Answers")
                    .FirstOrDefault(
                        t =>
                            !t.IsLocked && t.Id == test.Id &&
                            (t.ClosedDate == null || t.ClosedDate.Value > DateTime.Now) &&
                            (t.StartedDate == null || t.StartedDate.Value < DateTime.Now));
            if (testDb == null)
                return "Test not found";
            if (testDb.TestResults.Any(tr => tr.UserId == user.Student.FirstOrDefault().Id))
                return "Completed";
            var testResult = new TestResult
            {
                UserId = user.Student.FirstOrDefault().Id,
                TestId = testDb.Id,
                Correct = CheckCorrectAnswers(test.Questions.ToList(), testDb.Questions),
                Wrong = CheckWrongAnswers(test.Questions.ToList(), testDb.Questions)
            };
            _context.TestResults.Add(testResult);
            _context.SaveChanges();
            return "Done";
        }

        private int CheckCorrectAnswers(List<CheckTestQuestionViewModel> questionVm, List<Question> questionDb)
        {
            var result = 0;
            foreach (var question in questionVm)
            {
                var correctCount = 0;
                foreach (var answer in question.Answers)
                    if (answer.IsChecked ==
                        questionDb.FirstOrDefault(q => q.Id == question.Id)
                            .Answers.FirstOrDefault(a => a.Id == answer.Id)
                            .IsCorrect)
                        correctCount++;
                if (correctCount == question.Answers.Length)
                    result++;
            }
            return result;
        }

        private int CheckWrongAnswers(List<CheckTestQuestionViewModel> questionVm, List<Question> questionDb)
        {
            var result = 0;
            foreach (var question in questionVm)
            {
                var wrongCount = 0;
                foreach (var answer in question.Answers)
                    if (answer.IsChecked !=
                        questionDb.FirstOrDefault(q => q.Id == question.Id)
                            .Answers.FirstOrDefault(a => a.Id == answer.Id)
                            .IsCorrect)
                        wrongCount++;
                if (wrongCount > 0)
                    result++;
            }
            return result;
        }
    }
}