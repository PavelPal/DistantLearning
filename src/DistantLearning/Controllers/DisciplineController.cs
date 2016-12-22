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
                return "Invalid id";
            var discipline = await _context.Disciplines.FirstOrDefaultAsync(d => d.Id == id);
            if (discipline == null)
                return "Not found";
            return discipline;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("createDiscipline")]
        public async Task<object> CreateDiscipline([FromBody] string disciplineName)
        {
            if (string.IsNullOrEmpty(disciplineName))
                return "Invalid data";
            if (await _context.Disciplines.FirstOrDefaultAsync(d => d.Name.ToLower().Equals(disciplineName.ToLower())) !=
                null)
                return "Exist";
            var discipline = new Discipline(disciplineName);
            _context.Disciplines.Add(discipline);
            await _context.SaveChangesAsync();
            return new
            {
                Message = "Created",
                discipline.Id
            };
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("updateDiscipline")]
        public async Task<string> UpdateDiscipline([FromBody] Discipline discipline)
        {
            if (discipline == null)
                return "Invalid data";
            var dbDiscipline = await _context.Disciplines.FirstOrDefaultAsync(d => d.Id == discipline.Id);
            if (dbDiscipline == null)
                return "Not found";
            if (await _context.Disciplines.FirstOrDefaultAsync(d => d.Name.ToLower().Equals(discipline.Name.ToLower())) !=
                null)
                return "Exist";
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
                return "Invalid id";
            var discipline = await _context.Disciplines.FirstOrDefaultAsync(d => d.Id == id);
            if (discipline == null)
                return "Not found";
            _context.Disciplines.Remove(discipline);
            await _context.SaveChangesAsync();
            return "Deleted";
        }

        [HttpGet("teachersDisciplines/{id}")]
        public async Task<object> TeachersDisciplines(string id)
        {
            if (string.IsNullOrEmpty(id))
                return "Invalid id";
            var user =
                await _context.Users.Where(u => u.Id.Equals(id))
                    .Include("Teacher.Disciplines.Discipline")
                    .FirstOrDefaultAsync();
            if (user == null)
                return "Not found";
            return
                user.Teacher.FirstOrDefault()
                    .Disciplines.Select(teacherDiscipline => teacherDiscipline.Discipline)
                    .ToList();
        }
    }
}