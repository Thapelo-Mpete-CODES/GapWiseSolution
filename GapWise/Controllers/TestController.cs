using GapWise.Data;
using GapWise.Models;
using GapWise.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GapWise.Controllers
{
    public class TestController : Controller
    {
        private readonly DiagnosticService _diagnosticService;
        private readonly ApplicationDbContext _context;

        public TestController(DiagnosticService diagnosticService, ApplicationDbContext context)
        {
            _diagnosticService = diagnosticService;
            _context = context;
        }

        // Welcome Page (loads grades from DB)
        public IActionResult Welcome()
        {
            var grades = _context.Grades.ToList();
            return View(grades);
        }

        // Start Test
        [HttpPost]
        public IActionResult Start(int gradeId)
        {
            var studentTest = new StudentTest
            {
                StudentEmail = "demo@student.com",
                GradeId = gradeId,
                TestDate = DateTime.Now
            };

            _diagnosticService.CreateStudentTest(studentTest);

            TempData["StudentTestId"] = studentTest.Id;

            var questions = _diagnosticService.GenerateTest(gradeId);

            return View("TestPage", questions);
        }

        // Submit Test
        [HttpPost]
        public IActionResult Submit(int studentTestId, Dictionary<int, string> answers)
        {
            if (studentTestId == 0 || answers == null)
                return RedirectToAction("Welcome");

            var diagnosticResults = _diagnosticService.EvaluateTest(studentTestId, answers);

            return View("Result", diagnosticResults);
        }
    }
}