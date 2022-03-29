using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Quiz
{
    public class Statistics : PageModel
    {
        private readonly DAL.ApplicationDbContext _context;

        public Statistics(DAL.ApplicationDbContext context)
        {
            _context = context;
        }

        public Domain.Quiz Quiz { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Quiz = await _context.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(c => c.Choices)
                .FirstOrDefaultAsync(m => m.QuizId == id);

            if (Quiz == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}