using System.Collections.Generic;

namespace Domain.Model
{
    public class Question
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int Seconds { get; set; }

        public Test Test { get; set; }
        public int? TestId { get; set; }

        public List<Answer> Answers { get; set; }
    }
}