using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages_Choice
{
    public class IndexModel : PageModel
    {
        private readonly DAL.ApplicationDbContext _context;

        public IndexModel(DAL.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Choice> Choice { get;set; } = default!;
        public Question Question { get;set; } = default!;

        public async Task OnGetAsync(int id)
        {
            Choice = await _context.Choices
                .Include(c => c.Question)
                .Where(c => c.QuestionId == id)
                .ToListAsync();
            Question = await _context.Questions.FirstOrDefaultAsync(m => m.QuestionId == id);
        }
    }
}
