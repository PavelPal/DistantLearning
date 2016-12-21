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
            return await _context.Groups.OrderBy(g => g.Prefix).ThenBy(g => g.Postfix).ToListAsync();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<object> Group(int? id)
        {
            if (id == null)
                return "Invalid id";
            var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);
            if (group == null)
                return "Not found";
            return group;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("createGroup")]
        public async Task<object> CreateGroup([FromBody] int? prefix, [FromBody] string postfix)
        {
            if (string.IsNullOrEmpty(postfix) || prefix == null)
                return "Invalid data";
            if (await _context.Groups.FirstOrDefaultAsync(g => g.Prefix == prefix.Value && g.Postfix.Equals(postfix)) !=
                null)
                return "Exist";
            var group = new Group(prefix.Value, postfix);
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return new
            {
                Message = "Created",
                group.Id
            };
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("updateGroup")]
        public async Task<string> UpdateGroup([FromBody] Group group)
        {
            if (group == null)
                return "Invalid data";
            var dbGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Id == group.Id);
            if (dbGroup == null)
                return "Not found";
            dbGroup.Prefix = group.Prefix;
            dbGroup.Postfix = group.Postfix;
            _context.ChangeTracker.DetectChanges();
            await _context.SaveChangesAsync();
            return "Updated";
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("deleteGroup/{id}")]
        public async Task<string> DeleteGroup(int? id)
        {
            if (id == null)
                return "Invalid id";
            var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);
            if (group == null)
                return "Not found";
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return "Deleted";
        }

        [HttpGet("studentsGroup/{id}")]
        public async Task<object> StudentsGroup(string id)
        {
            if (string.IsNullOrEmpty(id))
                return "Invalid id";
            var user =
                await _context.Users.Where(u => u.Id.Equals(id))
                    .Include("Student.Group")
                    .FirstOrDefaultAsync();
            if (user == null)
                return "Not found";
            return user.Student.FirstOrDefault().Group;
        }
    }
}