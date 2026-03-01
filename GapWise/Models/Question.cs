using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GapWise.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string OptionA { get; set; }

        [Required]
        public string OptionB { get; set; }

        [Required]
        public string OptionC { get; set; }

        [Required]
        public string OptionD { get; set; }

        [Required]
        public string CorrectAnswer { get; set; } // A/B/C/D

        [Required]
        public DifficultyLevel Difficulty { get; set; } // Enum

        [Required]
        public int ConceptId { get; set; }

        [ForeignKey("ConceptId")]
        public Concept Concept { get; set; }

        // Navigation property: student answers linked to this question
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}