using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessProvider;
using DistantLearning.Models;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DistantLearning.Controllers
{
    [Authorize]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly DomainModelContext _context;
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager, DomainModelContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet("")]
        public async Task<List<UsersViewModel>> Users(string searchString, int skip, int take)
        {
            var users = new List<UsersViewModel>();
            var dbUsers = searchString == null
                ? await
                    _context.Users.OrderBy(u => u.FirstName).ThenBy(u => u.LastName).Skip(skip).Take(take).ToListAsync()
                : await
                    _context.Users.Where(
                            u =>
                                u.FirstName.Contains(searchString) || u.LastName.Contains(searchString) ||
                                searchString.Contains(u.FirstName) || searchString.Contains(u.LastName))
                        .Skip(skip)
                        .Take(take)
                        .ToListAsync();
            foreach (var user in dbUsers)
                users.Add(new UsersViewModel(user, await _userManager.GetRolesAsync(user)));
            return users;
        }
    }
}