using System;
using Domain.Model;

namespace DistantLearning.Models
{
    public class DocumentViewModel
    {
        public DocumentViewModel()
        {
        }

        public DocumentViewModel(Document document)
        {
            Id = document.Id;
            Name = document.Name;
            Date = document.Date;
            IsLocked = document.IsLocked;
        }

        public DocumentViewModel(Document document, User owner)
        {
            Id = document.Id;
            Name = document.Name;
            Date = document.Date;
            IsLocked = document.IsLocked;
            Owner = owner;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool IsLocked { get; set; }
        public User Owner { get; set; }
    }
}