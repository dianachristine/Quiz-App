using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL;
using Domain;

namespace WebApp.Pages_Question
{
    public class CreateModel : PageModel
    {
        private readonly DAL.ApplicationDbContext _context;

        public CreateModel(DAL.ApplicationDbContext context)
        {
            _context = context;
        }

        
        [BindProperty]
        public Question Question { get; set; }  = default!;
        
        public int QuizId { get; set; }  = default!;
        [BindProperty] public int QuizIdFromPost { get; set; }  = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            QuizId = id;
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
           /* if (!ModelState.IsValid)
            {
                return Page();
            }*/

            Question.QuizId = QuizIdFromPost;
            _context.Questions.Add(Question);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new {id = QuizIdFromPost});
        }
    }
}
