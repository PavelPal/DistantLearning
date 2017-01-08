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
    }
}