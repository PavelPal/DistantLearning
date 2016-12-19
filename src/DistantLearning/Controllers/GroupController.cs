using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessProvider;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DistantLearning.Controllers
{
    [Authorize]
    [Route("api/group")]
    public class GroupController : Controller
    {
        private readonly DomainModelContext _context;

        public GroupController(DomainModelContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        [AllowAnonymous]
        public async Task<List<Group>> Groups()
        {
            return await _context.Groups.OrderBy(g => g.Name).ToListAsync();
        }

        [HttpGet("studentsGroup/{id}")]
        public async Task<object> StudentsGroup(string id)
        {
            if (id == null)
                return "Incorrect id";
            var user =
                await _context.Users.Where(u => u.Id.Equals(id))
                    .Include("Student.Group")
                    .FirstOrDefaultAsync();
            if (user == null)
                return "User not found";
            return user.Student.FirstOrDefault().Group;
        }
    }
}