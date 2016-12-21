using System;

namespace Domain.Model
{
    public class PendingUserData
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public User User { get; set; }
        public string UserId { get; set; }
    }
}