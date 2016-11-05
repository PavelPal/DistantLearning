using System.Collections.Generic;

namespace Domain.Model
{
    public class Question
    {
        public Question()
        {
            Answers = new List<Answer>();
        }

        public int Id { get; set; }
        public Test Test { get; set; }
        public int TestId { get; set; }
        public string Body { get; set; }
        public int Seconds { get; set; }

        public List<Answer> Answers { get; set; }
    }
}