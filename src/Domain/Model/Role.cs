using System.Collections.Generic;

namespace Domain.Model
{
    public class Role
    {
        public Role()
        {
            Users = new List<UserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public List<UserRole> Users { get; set; }
    }
}