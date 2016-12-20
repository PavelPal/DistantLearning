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
            List<User> dbUsers;
            if (searchString == null)
            {
                dbUsers =
                    await
                        _context.Users.OrderBy(u => u.FirstName)
                            .ThenBy(u => u.LastName)
                            .Skip(skip)
                            .Take(take)
                            .ToListAsync();
            }
            else
            {
                var searchStringToLower = searchString.ToLower();
                dbUsers = await
                    _context.Users.Where(
                            u =>
                                u.FirstName.ToLower().Contains(searchStringToLower) ||
                                u.LastName.ToLower().Contains(searchStringToLower) ||
                                searchStringToLower.Contains(u.FirstName.ToLower()) ||
                                searchStringToLower.Contains(u.LastName.ToLower()))
                        .Skip(skip)
                        .Take(take)
                        .ToListAsync();
            }
            var users = new List<UsersViewModel>();
            foreach (var user in dbUsers)
                users.Add(new UsersViewModel(user, await _userManager.GetRolesAsync(user)));
            return users;
        }
    }
}