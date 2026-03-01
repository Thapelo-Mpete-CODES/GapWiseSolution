namespace GapWise.Models
{
    public class DiagnosticResult
    {
        public string ConceptName { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public double Percentage { get; set; }
        public string StrengthLevel { get; set; }
    }
}