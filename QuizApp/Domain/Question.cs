using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Question
    {
        public int QuestionId { get; set; }
        
        [MaxLength(500)]
        [Display(Name = "Question")]
        public string QuestionItself { get; set; }  = default!;
        
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; } = default!;
        
        public ICollection<Choice> Choices { get; set; }  = default!;
    }
}
