using System.Threading.Tasks;
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

        [Route("")]
        [HttpGet]
        public async Task<User> Profile()
        {
            return await _userManager.GetUserAsync(User);
        }
    }
}