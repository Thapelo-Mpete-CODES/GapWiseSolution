using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GapWise.Models
{
    public class StudentAnswer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentTestId { get; set; }

        [ForeignKey("StudentTestId")]
        public StudentTest StudentTest { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public Question Question { get; set; }

        [Required]
        public string SelectedAnswer { get; set; }

        [Required]
        public double Score { get; set; }

        [Required]
        public bool IsCorrect { get; set; }

        [Required]
        public string UserId { get; set; } // e.g., email or Identity user ID
    }
}