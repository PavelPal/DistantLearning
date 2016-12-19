using System;
using System.IO;
using System.Threading.Tasks;
using DataAccessProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DistantLearning.Controllers
{
    [Authorize]
    [Route("api/image")]
    public class ImageController : Controller
    {
        private readonly DomainModelContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ImageController(DomainModelContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("uploadProfileImage")]
        public async Task<string> UploadProfileImage(IFormFile file)
        {
            if (file == null || file.Length <= 0) return "Error";
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));
            if (user == null) return "Error";
            var path = Path.Combine(_hostingEnvironment.WebRootPath,
                "data" + Path.DirectorySeparatorChar + "profile_photos");
            try
            {
                RemoveExsitingImages(user.Id, path);
                var fileExtention = file.ContentType.Substring(file.ContentType.LastIndexOf('/') + 1);
                if (!fileExtention.Equals("jpeg") && !fileExtention.Equals("jpg") && !fileExtention.Equals("png"))
                    return "Incorrect file extension";
                var filename = user.Id + '.' + fileExtention;
                using (var fileStream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                user.PhotoPath = filename;
                _context.ChangeTracker.DetectChanges();
                await _context.SaveChangesAsync();
                return "Uploaded";
            }
            catch (Exception)
            {
                return "Error";
            }
        }

        private void RemoveExsitingImages(string name, string path)
        {
            var exsistingFiles = Directory.GetFiles(path, name + ".*");
            foreach (var exsistingFile in exsistingFiles)
                System.IO.File.Delete(exsistingFile);
        }
    }
}