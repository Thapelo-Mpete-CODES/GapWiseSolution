using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GapWise.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        // Navigation: Student has multiple test sessions
        public ICollection<StudentTest> StudentTests { get; set; }
    }
}