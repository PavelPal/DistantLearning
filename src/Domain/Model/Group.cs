using System.Collections.Generic;

namespace Domain.Model
{
    public class Group
    {
        public Group()
        {
            Students = new List<UserStudent>();
            Journals = new List<Journal>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public List<UserStudent> Students { get; set; }
        public List<Journal> Journals { get; set; }
    }
}