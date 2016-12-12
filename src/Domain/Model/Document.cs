using System;

namespace Domain.Model
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public bool IsLocked { get; set; } = false;
        public byte[] File { get; set; }
        public string FileType { get; set; }

        public UserTeacher Teacher { get; set; }
        public int TeacherId { get; set; }
    }
}