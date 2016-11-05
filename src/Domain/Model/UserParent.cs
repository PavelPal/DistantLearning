using System.Collections.Generic;

namespace Domain.Model
{
    public class UserParent
    {
        public UserParent()
        {
            Children = new List<ChildParent>();
        }

        public string Id { get; set; }
        public User User { get; set; }

        public List<ChildParent> Children { get; set; }
    }
}