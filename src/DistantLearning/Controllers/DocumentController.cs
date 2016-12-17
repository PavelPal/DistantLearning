using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataAccessProvider;
using DistantLearning.Models;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DistantLearning.Controllers
{
    [Authorize]
    [Route("api/document")]
    public class DocumentController : Controller
    {
        private readonly DomainModelContext _context;

        public DocumentController(DomainModelContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public async Task<List<Document>> Documents()
        {
            return await _context.Documents.OrderByDescending(d => d.Date).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<object> Document(int? id)
        {
            if (id == null)
                return "Некорректный id.";
            return await _context.Documents.FirstOrDefaultAsync(d => d.Id == id);
        }

        [HttpGet("byTeacher/{id}")]
        public async Task<object> TeachersDocuments(string id)
        {
            if (id == null)
                return "Некорректный id.";
            var user =
                await _context.Users.Where(u => u.Id.Equals(id)).Include("Teacher.Documents").FirstOrDefaultAsync();
            if (user == null)
                return "Пользователь не найден.";
            return
                user.Teacher.FirstOrDefault()
                    .Documents.OrderByDescending(d => d.Date)
                    .Select(document => new DocumentViewModel(document))
                    .ToList();
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost("uploadDocument")]
        public async Task<object> UploadDocument(IFormFile file)
        {
            if (file == null || file.Length <= 0) return "Ошибка";
            var user = await
                _context.Users.Include("Teacher.Documents")
                    .FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));
            if (user == null) return "Ошибка";
            try
            {
                byte[] buffer;
                using (var stream = file.OpenReadStream())
                {
                    buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, (int) stream.Length);
                }
                var base64FileRepresentation = Convert.ToBase64String(buffer);
                if (user.Teacher.FirstOrDefault().Documents == null)
                    user.Teacher.FirstOrDefault().Documents = new List<Document>();
                user.Teacher.FirstOrDefault().Documents.Add(new Document
                {
                    Name = file.FileName,
                    FileCode = base64FileRepresentation
                });
                _context.ChangeTracker.DetectChanges();
                await _context.SaveChangesAsync();
                return "Загружено";
            }
            catch (Exception)
            {
                return "Ошибка";
            }
        }

        [HttpGet("downloadDocument/{id}")]
        public async Task<object> Download(int? id)
        {
            if (id == null) return "Ошибка";
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == id);
            if (document == null) return "Ошибка";
            var bytes = Convert.FromBase64String(document.FileCode);
            var stream = new MemoryStream(bytes);
            var fileStream = new FileStreamResult(stream, "*/*") {FileDownloadName = document.Name};
            return fileStream;
        }
    }
}