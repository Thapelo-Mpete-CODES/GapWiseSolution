using GapWise.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using GapWise.Data;

namespace GapWise.Data
{
    /*
     DbInitializer
     ----------------
     Seeds the database with:
     - Grade 9
     - Concepts with learning order
     - 50 sample questions
    */
        public static class DbInitializer
        {
            public static void Initialize(ApplicationDbContext context)
            {
                // Ensure database exists
                context.Database.EnsureCreated();

                // -----------------------------
                // 1️⃣ Seed Grade 9
                // -----------------------------
                var grade = context.Grades.FirstOrDefault(g => g.Name == "Grade 9");
                if (grade == null)
                {
                    grade = new Grade { Name = "Grade 9" };
                    context.Grades.Add(grade);
                    context.SaveChanges();
                }

                // -----------------------------
                // 2️⃣ Seed Concepts with LearningOrder
                // -----------------------------
                if (!context.Concepts.Any(c => c.GradeId == grade.Id))
                {
                    context.Concepts.AddRange(
                        new Concept { Name = "Number Systems", GradeId = grade.Id, LearningOrder = 1 },
                        new Concept { Name = "Exponents", GradeId = grade.Id, LearningOrder = 2 },
                        new Concept { Name = "Algebraic Expressions", GradeId = grade.Id, LearningOrder = 3 },
                        new Concept { Name = "Linear Equations", GradeId = grade.Id, LearningOrder = 4 },
                        new Concept { Name = "Functions and Graphs", GradeId = grade.Id, LearningOrder = 5 },
                        new Concept { Name = "Geometry", GradeId = grade.Id, LearningOrder = 6 },
                        new Concept { Name = "Pythagoras", GradeId = grade.Id, LearningOrder = 7 },
                        new Concept { Name = "Area and Volume", GradeId = grade.Id, LearningOrder = 8 },
                        new Concept { Name = "Statistics", GradeId = grade.Id, LearningOrder = 9 },
                        new Concept { Name = "Probability", GradeId = grade.Id, LearningOrder = 10 }
                    );
                    context.SaveChanges();
                }

                // -----------------------------
                //50 Seed Questions
                // -----------------------------
                if (!context.Questions.Any())
                {
                    // Inside: context.Questions.AddRange( ... );
                    var concepts = context.Concepts.ToDictionary(c => c.Name, c => c.Id);

                    context.Questions.AddRange(
                        // =========================
                        // NUMBER SYSTEMS (5 questions)
                        // =========================
                        new Question { Text = "Convert 0.75 to a fraction", OptionA = "3/4", OptionB = "7/5", OptionC = "1/3", OptionD = "5/7", CorrectAnswer = "A", Difficulty = DifficultyLevel.Easy, ConceptId = concepts["Number Systems"] },
                        new Question { Text = "Write 25% as a decimal", OptionA = "0.25", OptionB = "2.5", OptionC = "0.025", OptionD = "25", CorrectAnswer = "A", Difficulty = DifficultyLevel.Easy, ConceptId = concepts["Number Systems"] },
                        new Question { Text = "Evaluate: 3/4 + 1/2", OptionA = "5/4", OptionB = "4/6", OptionC = "3/6", OptionD = "1/4", CorrectAnswer = "A", Difficulty = DifficultyLevel.Medium, ConceptId = concepts["Number Systems"] },
                        new Question { Text = "Simplify: (3/5) ÷ (2/3)", OptionA = "9/10", OptionB = "6/15", OptionC = "5/2", OptionD = "2/5", CorrectAnswer = "A", Difficulty = DifficultyLevel.Hard, ConceptId = concepts["Number Systems"] },
                        new Question { Text = "Express 7/20 as a percentage", OptionA = "35%", OptionB = "3.5%", OptionC = "7%", OptionD = "70%", CorrectAnswer = "A", Difficulty = DifficultyLevel.Medium, ConceptId = concepts["Number Systems"] },

                        // =========================
                        // EXPONENTS (5 questions)
                        // =========================
                        new Question { Text = "Evaluate: 2^3", OptionA = "6", OptionB = "8", OptionC = "9", OptionD = "12", CorrectAnswer = "B", Difficulty = DifficultyLevel.Easy, ConceptId = concepts["Exponents"] },
                        new Question { Text = "Simplify: 2^3 × 2^2", OptionA = "2^5", OptionB = "2^6", OptionC = "4^5", OptionD = "2^4", CorrectAnswer = "A", Difficulty = DifficultyLevel.Medium, ConceptId = concepts["Exponents"] },
                        new Question { Text = "Simplify: 2^5 × 2^3 ÷ 2^2", OptionA = "2^6", OptionB = "2^5", OptionC = "2^4", OptionD = "2^7", CorrectAnswer = "A", Difficulty = DifficultyLevel.Hard, ConceptId = concepts["Exponents"] },
                        new Question { Text = "Evaluate: 10^0", OptionA = "1", OptionB = "0", OptionC = "10", OptionD = "-1", CorrectAnswer = "A", Difficulty = DifficultyLevel.Easy, ConceptId = concepts["Exponents"] },
                        new Question { Text = "Simplify: (3^2)^3", OptionA = "3^5", OptionB = "3^6", OptionC = "9^3", OptionD = "3^9", CorrectAnswer = "D", Difficulty = DifficultyLevel.Medium, ConceptId = concepts["Exponents"] },

                        // =========================
                        // ALGEBRAIC EXPRESSIONS (5 questions)
                        // =========================
                        new Question { Text = "Simplify: 2x + 5x", OptionA = "7x", OptionB = "10x", OptionC = "5x^2", OptionD = "3x", CorrectAnswer = "A", Difficulty = DifficultyLevel.Easy, ConceptId = concepts["Algebraic Expressions"] },
                        new Question { Text = "Expand: 3(x + 2)", OptionA = "3x + 6", OptionB = "3x + 2", OptionC = "x + 6", OptionD = "3x^2", CorrectAnswer = "A", Difficulty = DifficultyLevel.Easy, ConceptId = concepts["Algebraic Expressions"] },
                        new Question { Text = "Expand: (x + 4)(x + 2)", OptionA = "x^2 + 6x + 8", OptionB = "x^2 + 8", OptionC = "2x^2 + 6x", OptionD = "x^2 + 4x", CorrectAnswer = "A", Difficulty = DifficultyLevel.Medium, ConceptId = concepts["Algebraic Expressions"] },
                        new Question { Text = "Factor: x^2 - 9", OptionA = "(x - 3)(x + 3)", OptionB = "(x - 9)(x + 1)", OptionC = "(x - 3)^2", OptionD = "Prime", CorrectAnswer = "A", Difficulty = DifficultyLevel.Hard, ConceptId = concepts["Algebraic Expressions"] },
                        new Question { Text = "Simplify: 4x - 2x + 7", OptionA = "2x + 7", OptionB = "6x + 7", OptionC = "2x - 7", OptionD = "4x + 5", CorrectAnswer = "A", Difficulty = DifficultyLevel.Easy, ConceptId = concepts["Algebraic Expressions"] },

                        // =========================
                        // LINEAR EQUATIONS (5 questions)
                        // =========================
                        new Question { Text = "Solve: 3x - 6 = 9", OptionA = "3", OptionB = "4", OptionC = "5", OptionD = "6", CorrectAnswer = "C", Difficulty = DifficultyLevel.Medium, ConceptId = concepts["Linear Equations"] },
                        new Question { Text = "Solve: 2(x - 3) + 4 = 3x", OptionA = "-2", OptionB = "2", OptionC = "4", OptionD = "0", CorrectAnswer = "A", Difficulty = DifficultyLevel.Hard, ConceptId = concepts["Linear Equations"] },
                        new Question { Text = "Solve: x/2 + 5 = 9", OptionA = "4", OptionB = "8", OptionC = "12", OptionD = "2", CorrectAnswer = "B", Difficulty = DifficultyLevel.Medium, ConceptId = concepts["Linear Equations"] },
                        new Question { Text = "Solve: 5x - 3 = 2x + 9", OptionA = "4", OptionB = "2", OptionC = "3", OptionD = "6", CorrectAnswer = "C", Difficulty = DifficultyLevel.Medium, ConceptId = concepts["Linear Equations"] },
                        new Question { Text = "Solve: 7x + 2 = 23", OptionA = "3", OptionB = "5", OptionC = "4", OptionD = "7", CorrectAnswer = "B", Difficulty = DifficultyLevel.Easy, ConceptId = concepts["Linear Equations"] },

                        // =========================
                        // FUNCTIONS AND GRAPHS (5 questions)
                        // =========================
                        new Question { Text = "What is the y-intercept of y = 2x + 3?", OptionA = "3", OptionB = "2", OptionC = "-3", OptionD = "0", CorrectAnswer = "A", Difficulty = DifficultyLevel.Easy, ConceptId = concepts["Functions and Graphs"] },
                        new Question { Text = "Slope of y = 5x - 4", OptionA = "5", OptionB = "-5", OptionC = "4", OptionD = "-4", CorrectAnswer = "A", Difficulty = DifficultyLevel.Easy, ConceptId = concepts["Functions and Graphs"] },
                        new Question { Text = "Graph passes through (0,0) and (2,4), find slope", OptionA = "2", OptionB = "4", OptionC = "1", OptionD = "0", CorrectAnswer = "A", Difficulty = DifficultyLevel.Medium, ConceptId = concepts["Functions and Graphs"] },
                        new Question { Text = "Find equation: slope=3, y-intercept= -2", OptionA = "y=3x-2", OptionB = "y=3x+2", OptionC = "y=x-2", OptionD = "y=-3x+2", CorrectAnswer = "A", Difficulty = DifficultyLevel.Medium, ConceptId = concepts["Functions and Graphs"] },
                        new Question { Text = "Equation of horizontal line through y=5", OptionA = "y=5", OptionB = "x=5", OptionC = "y=x", OptionD = "x=0", CorrectAnswer = "A", Difficulty = DifficultyLevel.Easy, ConceptId = concepts["Functions and Graphs"] },

                        // =========================
                        // GEOMETRY (5 questions)
                        // =========================
                        new Question { Text = "Sum of interior angles of a triangle", OptionA = "180°", OptionB = "360°", OptionC = "90°", OptionD = "270°", CorrectAnswer = "A", Difficulty = DifficultyLevel.Easy, ConceptId = concepts["Geometry"] },
                        new Question { Text = "A square has side 5. Find perimeter", OptionA = "20", OptionB = "25", OptionC = "10", OptionD = "15", CorrectAnswer = "A", Difficulty = DifficultyLevel.Easy, ConceptId = concepts["Geometry"] },
                        new Question { Text = "Area of rectangle 4x7", OptionA = "28", OptionB = "11", OptionC = "14", OptionD = "21", CorrectAnswer = "A", Difficulty = DifficultyLevel.Medium, ConceptId = concepts["Geometry"] },
                        new Question { Text = "Volume of cube with side 3", OptionA = "27", OptionB = "9", OptionC = "18", OptionD = "12", CorrectAnswer = "A", Difficulty = DifficultyLevel.Medium, ConceptId = concepts["Geometry"] },
                        new Question { Text = "Area of circle radius 7 (π≈3.14)", OptionA = "153.86", OptionB = "44", OptionC = "21", OptionD = "49", CorrectAnswer = "A", Difficulty = DifficultyLevel.Hard, ConceptId = concepts["Geometry"] },

                        // =========================
                        // PYTHAGORAS (5 questions)
                        // =========================
                        new Question { Text = "Hypotenuse of right triangle with sides 5 and 12", OptionA = "13", OptionB = "10", OptionC = "17", OptionD = "7", CorrectAnswer = "A", Difficulty = DifficultyLevel.Medium, ConceptId = concepts["Pythagoras"] },
                        new Question { Text = "Find missing side: 3-4-?", OptionA = "5", OptionB = "6", OptionC = "7", OptionD = "8", CorrectAnswer = "A", Difficulty = DifficultyLevel.Easy, ConceptId = concepts["Pythagoras"] },
                        new Question { Text = "Triangle sides: 6,8,hypotenuse?", OptionA = "10", OptionB = "12", OptionC = "9", OptionD = "14", CorrectAnswer = "A", Difficulty = DifficultyLevel.Medium, ConceptId = concepts["Pythagoras"] },
                        new Question { Text = "Sides 7,24, hypotenuse?", OptionA = "25", OptionB = "31", OptionC = "26", OptionD = "21", CorrectAnswer = "A", Difficulty = DifficultyLevel.Hard, ConceptId = concepts["Pythagoras"] },
                        new Question { Text = "Sides 8,15, hypotenuse?", OptionA = "17", OptionB = "16", OptionC = "23", OptionD = "19", CorrectAnswer = "A", Difficulty = DifficultyLevel.Medium, ConceptId = concepts["Pythagoras"] },

                        // =========================
                        // AREA AND VOLUME (5 questions)
                        // =========================
                        new Question { Text = "Area of triangle base=5, height=4", OptionA = "10", OptionB = "20", OptionC = "9", OptionD = "12", CorrectAnswer = "A", Difficulty = DifficultyLevel.Easy, ConceptId = concepts["Area and Volume"] },
                        new Question { Text = "Volume of cylinder r=3, h=5 (π≈3.14)", OptionA = "141.3", OptionB = "47.1", OptionC = "150", OptionD = "45", CorrectAnswer = "A", Difficulty = DifficultyLevel.Medium, ConceptId = concepts["Area and Volume"] },
                        new Question { Text = "Area of square side 6", OptionA = "36", OptionB = "12", OptionC = "18", OptionD = "24", CorrectAnswer = "A", Difficulty = DifficultyLevel.Easy, ConceptId = concepts["Area and Volume"] },
                        new Question { Text = "Volume of cube side 4", OptionA = "64", OptionB = "16", OptionC = "32", OptionD = "48", CorrectAnswer = "A", Difficulty = DifficultyLevel.Medium, ConceptId = concepts["Area and Volume"] },
                        new Question { Text = "Surface area of cube side 3", OptionA = "54", OptionB = "27", OptionC = "18", OptionD = "36", CorrectAnswer = "A", Difficulty = DifficultyLevel.Hard, ConceptId = concepts["Area and Volume"] },

                        // =========================
                        // STATISTICS (5 questions)
                        // =========================
                        new Question { Text = "Mean of 2,4,6,8", OptionA = "5", OptionB = "4", OptionC = "6", OptionD = "8", CorrectAnswer = "A", Difficulty = DifficultyLevel.Easy, ConceptId = concepts["Statistics"] },
                        new Question { Text = "Median of 3,7,9,12,5", OptionA = "7", OptionB = "5", OptionC = "9", OptionD = "6", CorrectAnswer = "A", Difficulty = DifficultyLevel.Medium, ConceptId = concepts["Statistics"] },
                        new Question { Text = "Mode of 2,2,3,4,2", OptionA = "2", OptionB = "3", OptionC = "4", OptionD = "1", CorrectAnswer = "A", Difficulty = DifficultyLevel.Easy, ConceptId = concepts["Statistics"] },
                        new Question { Text = "Range of 5,8,12,20", OptionA = "15", OptionB = "12", OptionC = "20", OptionD = "8", CorrectAnswer = "A", Difficulty = DifficultyLevel.Medium, ConceptId = concepts["Statistics"] },
                        new Question { Text = "Variance formula for sample", OptionA = "Σ(x-μ)^2/n-1", OptionB = "Σx/n", OptionC = "Σ(x-μ)^2/n", OptionD = "Σx^2/n", CorrectAnswer = "A", Difficulty = DifficultyLevel.Hard, ConceptId = concepts["Statistics"] },

                        // =========================
                        // PROBABILITY (5 questions)
                        // =========================
                        new Question { Text = "Probability of heads (fair coin)", OptionA = "1/2", OptionB = "1/4", OptionC = "1", OptionD = "0", CorrectAnswer = "A", Difficulty = DifficultyLevel.Easy, ConceptId = concepts["Probability"] },
                        new Question { Text = "Probability of rolling 4 on a die", OptionA = "1/6", OptionB = "1/4", OptionC = "1/5", OptionD = "1/2", CorrectAnswer = "A", Difficulty = DifficultyLevel.Easy, ConceptId = concepts["Probability"] },
                        new Question { Text = "Probability of rolling odd number", OptionA = "1/2", OptionB = "1/3", OptionC = "1/4", OptionD = "2/3", CorrectAnswer = "A", Difficulty = DifficultyLevel.Medium, ConceptId = concepts["Probability"] },
                        new Question { Text = "Probability of not getting 3 on a die", OptionA = "5/6", OptionB = "1/6", OptionC = "2/3", OptionD = "1/2", CorrectAnswer = "A", Difficulty = DifficultyLevel.Medium, ConceptId = concepts["Probability"] },
                        new Question { Text = "Probability of flipping 2 heads in 2 coin tosses", OptionA = "1/4", OptionB = "1/2", OptionC = "1/3", OptionD = "1", CorrectAnswer = "A", Difficulty = DifficultyLevel.Hard, ConceptId = concepts["Probability"] }
                    );

                    context.SaveChanges();
                }
            }
        }
    }

    