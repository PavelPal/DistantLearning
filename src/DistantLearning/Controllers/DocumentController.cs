﻿using System;
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
    [Route("api/document")]
    public class DocumentController : Controller
    {
        private readonly DomainModelContext _context;

        public DocumentController(DomainModelContext context)
        {
            _context = context;
        }

        [Route("")]
        [HttpGet]
        public async Task<List<Document>> Documents()
        {
            return await _context.Documents.ToListAsync();
        }

        [Route("{id}")]
        [HttpGet]
        public Document Document(int? id)
        {
            if (id == null)
                throw new Exception("Некорректный id.");
            return _context.Documents.FirstOrDefault(d => d.Id == id);
        }

        [Route("byTeacher/{id}")]
        [HttpGet]
        public async Task<List<Document>> TeachersDocuments(string id)
        {
            if (id == null)
                throw new Exception("Некорректный id.");
            var user = _context.Users.Where(u => u.Id.Equals(id)).Include(u => u.Teacher).FirstOrDefault();
            if (user == null)
                throw new Exception("Пользователь не найден.");
            return await _context.Documents.Where(d => d.TeacherId == user.Teacher.FirstOrDefault().Id).ToListAsync();
        }
    }
}