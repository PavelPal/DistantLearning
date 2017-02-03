using System;

namespace Domain.Model
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public bool IsLocked { get; set; } = false;
        public string FileCode { get; set; }

        public UserTeacher Teacher { get; set; }
        public int TeacherId { get; set; }
    }
}