using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessProvider;
using DistantLearning.Models;
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
        public async Task<List<TestViewModel>> Tests()
        {
            return
                await _context.Tests.Include("Discipline")
                    .Include("Teacher.User")
                    .OrderByDescending(t => t.CreatedDate)
                    .Select(test => new TestViewModel(test))
                    .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<object> Test(int? id)
        {
            if (id == null)
                return "Incorrect id";
            var test = await _context.Tests.Include("Questions.Answers").FirstOrDefaultAsync(t => t.Id == id);
            if (test == null)
                return "Test not found";
            return test;
        }
    }
}