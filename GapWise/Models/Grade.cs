using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GapWise.Models
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Navigation property: list of concepts in this grade
        public ICollection<Concept> Concepts { get; set; }
    }
}