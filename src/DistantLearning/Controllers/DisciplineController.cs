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
    [Route("api/discipline")]
    public class DisciplineController : Controller
    {
        private readonly DomainModelContext _context;

        public DisciplineController(DomainModelContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        [AllowAnonymous]
        public async Task<List<Discipline>> Disciplines()
        {
            return await _context.Disciplines.OrderBy(d => d.Name).ToListAsync();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<object> Discipline(int? id)
        {
            if (id == null)
                return "Incorrect id";
            var discipline = await _context.Disciplines.FirstOrDefaultAsync(d => d.Id == id);
            if (discipline == null)
                return "Discipline not found";
            return discipline;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("createDiscipline")]
        public async Task<string> CreateDiscipline([FromBody] string disciplineName)
        {
            if (string.IsNullOrEmpty(disciplineName))
                return "Incorrect data";
            _context.Disciplines.Add(new Discipline(disciplineName));
            await _context.SaveChangesAsync();
            return "Created";
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("updateDiscipline")]
        public async Task<string> UpdateDiscipline([FromBody] Discipline discipline)
        {
            if (discipline == null)
                return "Incorrect data";
            var dbDiscipline = await _context.Disciplines.FirstOrDefaultAsync(d => d.Id == discipline.Id);
            if (dbDiscipline == null)
                return "Discipline not found";
            dbDiscipline.Name = discipline.Name;
            _context.ChangeTracker.DetectChanges();
            await _context.SaveChangesAsync();
            return "Updated";
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("deleteDiscipline/{id}")]
        public async Task<string> DeleteDiscipline(int? id)
        {
            if (id == null)
                return "Incorrect id";
            var discipline = await _context.Disciplines.FirstOrDefaultAsync(d => d.Id == id);
            if (discipline == null)
                return "Discipline not found";
            _context.Disciplines.Remove(discipline);
            await _context.SaveChangesAsync();
            return "Deleted";
        }

        [HttpGet("teachersDisciplines/{id}")]
        public async Task<object> TeachersDisciplines(string id)
        {
            if (id == null)
                return "Incorrect id";
            var user =
                await _context.Users.Where(u => u.Id.Equals(id))
                    .Include("Teacher.Disciplines.Discipline")
                    .FirstOrDefaultAsync();
            if (user == null)
                return "User not found";
            return
                user.Teacher.FirstOrDefault()
                    .Disciplines.Select(teacherDiscipline => teacherDiscipline.Discipline)
                    .ToList();
        }
    }
}