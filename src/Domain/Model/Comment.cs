using System;

namespace Domain.Model
{
    public class Comment
    {
        public Comment()
        {
            Date = DateTime.Now;
        }

        public int Id { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public Test Test { get; set; }
        public int TestId { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
    }
}