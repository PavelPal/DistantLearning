using System.Collections.Generic;
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

        [Route("")]
        [HttpGet]
        public async Task<List<UsersViewModel>> Users()
        {
            var users = new List<UsersViewModel>();
            foreach (var user in await _context.Users.ToListAsync())
                users.Add(new UsersViewModel
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    Id = user.Id,
                    LastName = user.LastName,
                    Photo = user.Photo,
                    PhotoType = user.PhotoType,
                    Roles = await _userManager.GetRolesAsync(user)
                });
            return users;
        }
    }
}