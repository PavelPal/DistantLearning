using System.Linq;
using System.Threading.Tasks;
using DataAccessProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DistantLearning.Controllers
{
    [Authorize]
    [Route("api/consultation")]
    public class ConsultationController : Controller
    {
        private readonly DomainModelContext _context;

        public ConsultationController(DomainModelContext context)
        {
            _context = context;
        }

        [HttpGet("byTeacher/{id}")]
        public async Task<object> TeachersConsultations(string id)
        {
            if (id == null)
                return "Некорректный id.";
            var user = _context.Users.Where(u => u.Id.Equals(id)).Include(u => u.Teacher).FirstOrDefault();
            if (user == null)
                return "Пользователь не найден.";
            return
                await _context.Consultations.Where(d => d.TeacherId == user.Teacher.FirstOrDefault().Id).ToListAsync();
        }
    }
}