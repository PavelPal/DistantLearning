﻿using System;
using System.Collections.Generic;

namespace Domain.Model
{
    public class Journal
    {
        public int Id { get; set; }
        public DateTime ActivatedDate { get; set; } = DateTime.Now;
        public DateTime ClosedDate { get; set; }

        public Group Group { get; set; }
        public int GroupId { get; set; }

        public List<Mark> Marks { get; set; }
        public List<JournalDiscipline> Disciplines { get; set; }
    }
}