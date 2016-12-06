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
    }
}