using System.Collections.Generic;
using Domain.Model;

namespace DistantLearning.Models
{
    public class ProfileViewModel
    {
        public ProfileViewModel()
        {
        }

        public ProfileViewModel(User user, IList<string> roles)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            EmailConfirmed = user.EmailConfirmed;
            PhoneNumber = user.PhoneNumber;
            PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            Photo = user.PhotoPath;
            Roles = roles;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string Photo { get; set; }
        public IList<string> Roles { get; set; }
    }
}