using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GapWise.Models
{
    public class Concept
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int GradeId { get; set; }

        [ForeignKey("GradeId")]
        public Grade Grade { get; set; }

        // Learning order within the grade
        public int LearningOrder { get; set; }

        // Navigation property: list of questions in this concept
        public ICollection<Question> Questions { get; set; }
    }
}