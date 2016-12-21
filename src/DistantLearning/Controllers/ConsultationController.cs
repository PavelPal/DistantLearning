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
            if (string.IsNullOrEmpty(id))
                return "Invalid id";
            var user =
                await _context.Users.Where(u => u.Id.Equals(id)).Include("Teacher.Consultations").FirstOrDefaultAsync();
            if (user == null)
                return "Not found";
            return user.Teacher.FirstOrDefault().Consultations.OrderBy(c => c.DayOfWeek);
        }

        [HttpPost("createConsultation")]
        public async Task<object> CreateConsultation([FromBody] Consultation consultation)
        {
            if (consultation == null)
                return "Invalid data";
            var user =
                await _context.Users.Include("Teacher.Consultations")
                    .FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));
            if (user == null)
                return "Not found";
            if (user.Teacher.FirstOrDefault().Consultations == null)
                user.Teacher.FirstOrDefault().Consultations = new List<Consultation>();
            user.Teacher.FirstOrDefault().Consultations.Add(consultation);
            _context.ChangeTracker.DetectChanges();
            await _context.SaveChangesAsync();
            return "Created";
        }
    }
}