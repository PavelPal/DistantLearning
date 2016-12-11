using System.Collections.Generic;
using Domain.Model;

namespace DistantLearning.Models
{
    public class UsersViewModel
    {
        public UsersViewModel()
        {
        }

        public UsersViewModel(User user, IList<string> roles)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            PhotoPath = user.PhotoPath;
            Roles = roles;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoPath { get; set; }
        public IList<string> Roles { get; set; }
    }
}