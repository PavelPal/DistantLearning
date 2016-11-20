﻿using System.Collections.Generic;

namespace Domain.Model
{
    public class UserParent
    {
        public UserParent()
        {
            Children = new List<ChildParent>();
        }

        public int Id { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }

        public List<ChildParent> Children { get; set; }
    }
}