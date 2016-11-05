using System;

namespace Domain.Model
{
    public class Document
    {
        public Document()
        {
            IsLocked = false;
            Date = DateTime.Now;
        }

        public int Id { get; set; }
        public UserTeacher Teacher { get; set; }
        public string TeacherId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool IsLocked { get; set; }
        public byte[] File { get; set; }
        public string FileType { get; set; }
    }
}