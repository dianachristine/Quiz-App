using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.AnswerQuiz
{
    public class Answer : PageModel
    {
        private readonly DAL.ApplicationDbContext _context;

        public Answer(DAL.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string? QuizId { get; set; }
        
        [BindProperty]
        public string? UserSelectedChoiceId { get; set; }

        [BindProperty] public string? NumOfAnsweredQuestions { get; set; }

        public Domain.Quiz Quiz { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (UserSelectedChoiceId != null)

            {
                var choice = await _context.Choices.FirstOrDefaultAsync(m => m.ChoiceId == int.Parse(UserSelectedChoiceId));
                choice.NumberOfAnswers += 1;

                _context.Attach(choice).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChoiceExists(choice.ChoiceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            
            if (QuizId != null)
            {
                Quiz = await _context.Quizzes
                    .Include(q => q.Questions)
                    .ThenInclude(c => c.Choices)
                    .FirstOrDefaultAsync(m => m.QuizId == int.Parse(QuizId));

                if (Quiz.Questions.Count == int.Parse(NumOfAnsweredQuestions ?? "0"))
                {
                    return Redirect("/Index");
                }
            }

            return Page();
        }

        private bool ChoiceExists(int id)
        {
            return _context.Choices.Any(e => e.ChoiceId == id);
        }
    }
}
