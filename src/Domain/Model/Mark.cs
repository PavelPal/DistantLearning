using System;
using System.Collections.Generic;

namespace Domain.Model
{
    public class Mark
    {
        public int Id { get; set; }
        public int Point { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public Journal Journal { get; set; }
        public int JournalId { get; set; }
        public Discipline Discipline { get; set; }
        public int DisciplineId { get; set; }

        public List<UserMark> Users { get; set; }
    }
}