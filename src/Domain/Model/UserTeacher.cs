using System.Collections.Generic;

namespace Domain.Model
{
    public class UserTeacher
    {
        public UserTeacher()
        {
            Disciplines = new List<TeacherDiscipline>();
            Tests = new List<Test>();
            Documents = new List<Document>();
            Consultations = new List<Consultation>();
        }

        public string Id { get; set; }
        public User User { get; set; }

        public List<TeacherDiscipline> Disciplines { get; set; }
        public List<Test> Tests { get; set; }
        public List<Document> Documents { get; set; }
        public List<Consultation> Consultations { get; set; }
    }
}