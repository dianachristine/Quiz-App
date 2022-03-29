using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Quiz
    {
        public int QuizId { get; set; }
        
        [MaxLength(200)]
        [Display(Name = "Name")]
        public string Name { get; set; }  = default!;
        
        [MaxLength(500)]
        [Display(Name = "Description")]
        public string Description { get; set; }  = default!;
        
        [Display(Name = "Type")]
        public EType Type { get; set; }
        
        public ICollection<Question> Questions { get; set; }  = default!;
    }
}
