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

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<object> Group(int? id)
        {
            if (id == null)
                return "Incorrect id";
            var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);
            if (group == null)
                return "Group not found";
            return group;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("createGroup")]
        public async Task<string> CreateGroup([FromBody] string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
                return "Incorrect data";
            _context.Groups.Add(new Group(groupName));
            await _context.SaveChangesAsync();
            return "Created";
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("updateGroup")]
        public async Task<string> UpdateGroup([FromBody] Group group)
        {
            if (group == null)
                return "Incorrect data";
            var dbGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Id == group.Id);
            if (dbGroup == null)
                return "Group not found";
            dbGroup.Name = group.Name;
            _context.ChangeTracker.DetectChanges();
            await _context.SaveChangesAsync();
            return "Updated";
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("deleteGroup/{id}")]
        public async Task<string> DeleteGroup(int? id)
        {
            if (id == null)
                return "Incorrect id";
            var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);
            if (group == null)
                return "Group not found";
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return "Deleted";
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