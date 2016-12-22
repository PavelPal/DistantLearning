using System.Collections.Generic;

namespace Domain.Model
{
    public class Group
    {
        public Group()
        {
        }

        public Group(int prefix, string postfix)
        {
            Prefix = prefix;
            Postfix = postfix;
        }

        public int Id { get; set; }
        public int Prefix { get; set; }
        public string Postfix { get; set; }

        public List<UserStudent> Students { get; set; }
        public List<Journal> Journals { get; set; }
    }
}