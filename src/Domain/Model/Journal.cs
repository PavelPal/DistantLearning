using System;
using System.Collections.Generic;

namespace Domain.Model
{
    public class Journal
    {
        public Journal()
        {
            Marks = new List<Mark>();
            Disciplines = new List<JournalDiscipline>();
            ActivatedDate = DateTime.Now;
        }

        public int Id { get; set; }
        public Group Group { get; set; }
        public int GroupId { get; set; }
        public DateTime ActivatedDate { get; set; }
        public DateTime ClosedDate { get; set; }

        public List<Mark> Marks { get; set; }
        public List<JournalDiscipline> Disciplines { get; set; }
    }
}