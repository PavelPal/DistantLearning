using System.Collections.Generic;

namespace Domain.Model
{
    public class Discipline
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Mark> Marks { get; set; }
        public List<JournalDiscipline> Journal { get; set; }
        public List<Test> Tests { get; set; }
        public List<TeacherDiscipline> Teachers { get; set; }
    }
}