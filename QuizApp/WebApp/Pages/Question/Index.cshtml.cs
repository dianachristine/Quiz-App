using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages_Question
{
    public class IndexModel : PageModel
    {
        private readonly DAL.ApplicationDbContext _context;

        public IndexModel(DAL.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Question> Question { get;set; } = default!;
        public Quiz Quiz { get;set; } = default!;

        public async Task OnGetAsync(int id)
        {
            Question = await _context.Questions
                .Include(q => q.Quiz)
                .Where(q => q.QuizId == id)
                .ToListAsync();
            Quiz = await _context.Quizzes.FirstOrDefaultAsync(m => m.QuizId == id);
        }
    }
}
