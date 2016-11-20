using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DistantLearning.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public int Type { get; set; }

        public List<int> Disciplines { get; set; }
        public List<string> Children { get; set; }
        public int? Group { get; set; }
    }
}