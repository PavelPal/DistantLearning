using System.Collections.Generic;

namespace Domain.Model
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<UserStudent> Students { get; set; }
        public List<Journal> Journals { get; set; }
    }
}