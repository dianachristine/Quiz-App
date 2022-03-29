using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL;
using Domain;

namespace WebApp.Pages_Choice
{
    public class CreateModel : PageModel
    {
        private readonly DAL.ApplicationDbContext _context;

        public CreateModel(DAL.ApplicationDbContext context)
        {
            _context = context;
        }
        

        [BindProperty]
        public Choice Choice { get; set; } = default!;
        
        [BindProperty]
        public bool IsCorrect { get; set; } = default!;
        
        public int QuestionId { get; set; }  = default!;
        [BindProperty] public int QuestionIdFromPost { get; set; }  = default!;
        
        public async Task<IActionResult> OnGetAsync(int id)
        {
            QuestionId = id;
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            /*if (!ModelState.IsValid)
            {
                return Page();
            }*/

            Choice.IsCorrectChoice = IsCorrect;
            Choice.QuestionId = QuestionIdFromPost;
            _context.Choices.Add(Choice);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new {id = QuestionIdFromPost});
        }
    }
}
