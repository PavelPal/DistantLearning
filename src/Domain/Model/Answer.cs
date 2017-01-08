namespace Domain.Model
{
    public class Answer
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public bool IsCorrect { get; set; }

        public Question Question { get; set; }
        public int? QuestionId { get; set; }
    }
}