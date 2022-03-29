using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Choice
    {
        public int ChoiceId { get; set; }
        
        [MaxLength(500)]
        [Display(Name = "Choice")]
        public string ChoiceItself { get; set; }  = default!;
        
        public bool? IsCorrectChoice { get; set; }

        public int NumberOfAnswers { get; set; } = 0;
        
        public int QuestionId { get; set; }
        public Question Question { get; set; } = default!;
    }
}
