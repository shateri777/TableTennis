using System.ComponentModel.DataAnnotations;

namespace TableTennis.ViewModels
{
    public class MatchFormVM
    {

        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Namnet måste vara minst 2 bokstäver, max 50.")]
        public string Player1FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Namnet måste vara minst 2 bokstäver, max 50.")]
        public string Player1LastName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Namnet måste vara minst 2 bokstäver, max 50.")]
        public string Player2FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Namnet måste vara minst 2 bokstäver, max 50.")]
        public string Player2LastName { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Åldern måste vara mellan 1 och 100.")]
        public int Player1Age { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Åldern måste vara mellan 1 och 100.")]
        public int Player2Age { get; set; }
        public string SetGender { get; set; }
        public int BestOfSets { get; set; }
    }
}
