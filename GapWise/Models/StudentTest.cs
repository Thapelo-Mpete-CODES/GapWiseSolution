using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GapWise.Models
{
    public class StudentTest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string StudentEmail { get; set; }

        [Required]
        public int GradeId { get; set; }

        [ForeignKey("GradeId")]
        public Grade Grade { get; set; }

        [Required]
        public DateTime TestDate { get; set; }

        // Navigation property: answers for this test
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}