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
        public async Task<List<DocumentViewModel>> Documents(string searchString, int skip, int take)
        {
            List<Document> dbDocuments;
            if (string.IsNullOrEmpty(searchString))
            {
                dbDocuments =
                    await _context.Documents.Include("Teacher.User")
                        .OrderByDescending(d => d.Date)
                        .Skip(skip)
                        .Take(take)
                        .ToListAsync();
            }
            else
            {
                var searchStringToLower = searchString.ToLower();
                dbDocuments = await _context.Documents.Include("Teacher.User").Where(
                        d =>
                            d.Name.ToLower().Contains(searchStringToLower) ||
                            searchStringToLower.Contains(d.Name.ToLower()))
                    .OrderByDescending(d => d.Date)
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
            }
            return dbDocuments.Select(document => new DocumentViewModel(document, document.Teacher.User)).ToList();
        }

        [HttpGet("byTeacher/{id}")]
        public async Task<object> TeachersDocuments(string id)
        {
            if (string.IsNullOrEmpty(id))
                return "Invalid id";
            var user =
                await _context.Users.Where(u => u.Id.Equals(id)).Include("Teacher.Documents").FirstOrDefaultAsync();
            if (user == null)
                return "Not found";
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
            if (file == null || file.Length <= 0) return "Error";
            var user = await
                _context.Users.Include("Teacher.Documents")
                    .FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));
            if (user == null) return "Not found";
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
                return "Uploaded";
            }
            catch (Exception)
            {
                return "Error";
            }
        }

        [HttpGet("downloadDocument/{id}")]
        public async Task<object> Download(int? id)
        {
            if (id == null) return "Invalid id";
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == id);
            if (document == null) return "Not found";
            var bytes = Convert.FromBase64String(document.FileCode);
            var stream = new MemoryStream(bytes);
            var fileStream = new FileStreamResult(stream, "*/*") {FileDownloadName = document.Name};
            return fileStream;
        }

        [Authorize(Roles = "Teacher, Admin")]
        [HttpPost("deleteDocument/{id}")]
        public async Task<object> DeleteDocument(int? id)
        {
            if (id == null)
                return "Invalid id";
            if (User.IsInRole("Admin"))
            {
                var doc = await _context.Documents.FirstOrDefaultAsync(d => d.Id == id);
                if (doc == null)
                    return "Not found";
                _context.Documents.Remove(doc);
            }
            else
            {
                var user =
                    await _context.Users.Include("Teacher.Documents")
                        .FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));
                if (user == null)
                    return "User not found";
                if (user.Teacher.FirstOrDefault().Documents.FirstOrDefault(d => d.Id == id) == null)
                    return "Document not found";
                _context.Documents.Remove(user.Teacher.FirstOrDefault().Documents.FirstOrDefault(d => d.Id == id));
            }
            _context.ChangeTracker.DetectChanges();
            await _context.SaveChangesAsync();
            return "Deleted";
        }
    }
}