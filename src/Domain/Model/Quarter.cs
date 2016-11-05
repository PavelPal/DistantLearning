using System;

namespace Domain.Model
{
    public class Quarter
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime ActivatedDate { get; set; }
        public DateTime ClosedDate { get; set; }
    }
}