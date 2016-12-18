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

        [HttpGet("teachersDisciplines/{id}")]
        public async Task<object> TeachersDisciplines(string id)
        {
            if (id == null)
                return "Некорректный id";
            var user =
                await _context.Users.Where(u => u.Id.Equals(id))
                    .Include("Teacher.Disciplines.Discipline")
                    .FirstOrDefaultAsync();
            if (user == null)
                return "Пользователь не найден";
            return
                user.Teacher.FirstOrDefault()
                    .Disciplines.Select(teacherDiscipline => teacherDiscipline.Discipline)
                    .ToList();
        }
    }
}