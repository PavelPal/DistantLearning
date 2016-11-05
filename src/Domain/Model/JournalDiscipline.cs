namespace Domain.Model
{
    public class JournalDiscipline
    {
        public Journal Journal { get; set; }
        public int JournalId { get; set; }
        public Discipline Discipline { get; set; }
        public int DisciplineId { get; set; }
    }
}