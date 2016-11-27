using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DistantLearning.Controllers
{
    [Authorize]
    [Route("api/image")]
    public class ImageController : Controller
    {
        private readonly UserManager<User> _userManager;

        public ImageController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
    }
}
