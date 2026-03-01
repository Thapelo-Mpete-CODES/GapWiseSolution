using GapWise.Data;
using Microsoft.EntityFrameworkCore;

namespace YourProject.Services
{
    public class WeaknessAnalysisService
    {
        private readonly ApplicationDbContext _context;

        public WeaknessAnalysisService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetWeakConceptsAsync(string userId)
        {
            var results = await _context.StudentAnswers
                .Where(u => u.UserId == userId)
                .Include(u => u.Question)
                .ThenInclude(q => q.Concept)
                .ToListAsync();

            var grouped = results
                .GroupBy(r => r.Question.Concept.Name)
                .Select(g => new
                {
                    Concept = g.Key,
                    Accuracy = g.Count(x => x.IsCorrect) * 100.0 / g.Count()
                })
                .ToList();

            return grouped
                .Where(g => g.Accuracy < 60)   // weakness threshold
                .Select(g => g.Concept)
                .ToList();
        }
    }
}