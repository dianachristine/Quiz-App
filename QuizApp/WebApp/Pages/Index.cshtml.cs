using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DAL.ApplicationDbContext _context;

        public IndexModel(DAL.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Domain.Quiz> Quiz { get;set; } = default!;
        
        public string? SearchName { get; set; }
        
        public async Task OnGetAsync(string? searchName, string action)
        {
            if (action == "Quizzes")
            {
                Quiz = await _context.Quizzes
                    .Include(q => q.Questions)
                    .ThenInclude(c => c.Choices)
                    .Where(q => q.Type == EType.Quiz)
                    .ToListAsync();
            } 
            else if (action == "Polls")
            {
                Quiz = await _context.Quizzes
                    .Include(q => q.Questions)
                    .ThenInclude(c => c.Choices)
                    .Where(q => q.Type == EType.Poll)
                    .ToListAsync();
            }
            else if (action == "All")
            {
                Quiz = await _context.Quizzes
                    .Include(q => q.Questions)
                    .ThenInclude(c => c.Choices)
                    .ToListAsync();
            }
            else if (action == "Clear")
            {
                searchName = null;
                Quiz = await _context.Quizzes.ToListAsync();
            }
            else
            {
                SearchName = searchName;

                var searchQuery = _context.Quizzes
                    .Include(x => x.Questions!)
                    .ThenInclude(y => y.Choices)
                    .AsQueryable();
                if (!string.IsNullOrWhiteSpace(searchName))
                {
                    searchName = searchName.Trim();
                    searchQuery = searchQuery.Where(q => 
                        q.Name.ToUpper().Contains(searchName.ToUpper()) ||
                        q.Description.ToUpper().Contains(searchName.ToUpper()));
            
                    Quiz = await searchQuery.ToListAsync();
                }
                else
                {
                    Quiz = await _context.Quizzes.ToListAsync();
                }
            }

        }
    }
}
