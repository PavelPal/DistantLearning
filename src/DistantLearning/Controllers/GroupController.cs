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
    }
}