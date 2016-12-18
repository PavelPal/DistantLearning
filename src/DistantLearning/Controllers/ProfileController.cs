using System.Threading.Tasks;
using DistantLearning.Models;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DistantLearning.Controllers
{
    [Authorize]
    [Route("api/profile")]
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;

        public ProfileController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("{id?}")]
        public async Task<object> Profile(string id)
        {
            if (id == null)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                return new ProfileViewModel(currentUser, await _userManager.GetRolesAsync(currentUser));
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return "Пользователь не найден";
            return new ProfileViewModel(user, await _userManager.GetRolesAsync(user));
        }
    }
}