namespace Domain.Model
{
    public class TestResult
    {
        public int Id { get; set; }
        public int Correct { get; set; } = 0;
        public int Wrong { get; set; } = 0;
        public int InComplete { get; set; } = 0;

        public Test Test { get; set; }
        public int? TestId { get; set; }
        public UserStudent User { get; set; }
        public int UserId { get; set; }
    }
}