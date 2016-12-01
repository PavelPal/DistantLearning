using System.Collections.Generic;

namespace DistantLearning.Models
{
    public class UsersViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoType { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }
}