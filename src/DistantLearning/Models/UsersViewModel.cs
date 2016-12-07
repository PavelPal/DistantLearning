﻿using System.Collections.Generic;
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
            Photo = user.Photo;
            PhotoType = user.PhotoType;
            Roles = roles;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoType { get; set; }
        public IList<string> Roles { get; set; }
    }
}