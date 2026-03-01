using GapWise.Data;
using GapWise.Models;
using Microsoft.EntityFrameworkCore;

namespace GapWise.Services
{
    public class DiagnosticService
    {
        private readonly ApplicationDbContext _context;

        public DiagnosticService(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1️⃣ Create Student Test
        public void CreateStudentTest(StudentTest test)
        {
            _context.StudentTests.Add(test);
            _context.SaveChanges();
        }

        // 2️⃣ Generate Questions by Grade
        public List<Question> GenerateTest(int gradeId)
        {
            return _context.Questions
                .Include(q => q.Concept)
                .Where(q => q.Concept.GradeId == gradeId)
                .OrderBy(x => Guid.NewGuid())
                .Take(20)
                .ToList();
        }

        // 3️⃣ Evaluate Test
        public List<DiagnosticResult> EvaluateTest(int studentTestId, Dictionary<int, string> answers)
        {
            var studentTest = _context.StudentTests
                .Include(st => st.StudentAnswers)
                .FirstOrDefault(st => st.Id == studentTestId);

            if (studentTest == null)
                return new List<DiagnosticResult>();

            foreach (var entry in answers)
            {
                var question = _context.Questions
                    .Include(q => q.Concept)
                    .FirstOrDefault(q => q.Id == entry.Key);

                if (question == null)
                    continue;

                bool isCorrect = question.CorrectAnswer == entry.Value;

                var studentAnswer = new StudentAnswer
                {
                    StudentTestId = studentTestId,
                    QuestionId = question.Id,
                    SelectedAnswer = entry.Value,
                    IsCorrect = isCorrect,
                    UserId = studentTest.StudentEmail
                };

                _context.StudentAnswers.Add(studentAnswer);
            }

            _context.SaveChanges();

            // 🔥 DIAGNOSTIC ANALYSIS

            var results = _context.StudentAnswers
                .Include(sa => sa.Question)
                    .ThenInclude(q => q.Concept)
                .Where(sa => sa.StudentTestId == studentTestId)
                .GroupBy(sa => sa.Question.Concept.Name)
                .Select(group => new DiagnosticResult
                {
                    ConceptName = group.Key,
                    TotalQuestions = group.Count(),
                    CorrectAnswers = group.Count(x => x.IsCorrect),
                    Percentage = (double)group.Count(x => x.IsCorrect) / group.Count() * 100,
                    StrengthLevel = ""
                })
                .ToList();

            // Assign Strength Labels
            foreach (var r in results)
            {
                if (r.Percentage >= 80)
                    r.StrengthLevel = "Strong";
                else if (r.Percentage >= 50)
                    r.StrengthLevel = "Moderate";
                else
                    r.StrengthLevel = "Weak";
            }

            return results;
        }
        // 4️⃣ Get Full Test Details
        public StudentTest GetTestDetails(int studentTestId)
        {
            return _context.StudentTests
                .Include(st => st.StudentAnswers)
                    .ThenInclude(sa => sa.Question)
                .FirstOrDefault(st => st.Id == studentTestId);
        }
    }
}