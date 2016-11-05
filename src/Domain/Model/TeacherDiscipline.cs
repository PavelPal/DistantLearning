namespace Domain.Model
{
    public class TeacherDiscipline
    {
        public UserTeacher Teacher { get; set; }
        public int TeacherId { get; set; }
        public Discipline Discipline { get; set; }
        public int DisciplineId { get; set; }
    }
}