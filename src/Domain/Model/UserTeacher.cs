using System.Collections.Generic;

namespace Domain.Model
{
    public class UserTeacher
    {
        public int Id { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }

        public List<TeacherDiscipline> Disciplines { get; set; }
        public List<Test> Tests { get; set; }
        public List<Document> Documents { get; set; }
        public List<Consultation> Consultations { get; set; }
    }
}