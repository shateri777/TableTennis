using System.ComponentModel.DataAnnotations;

namespace TableTennis.ViewModels
{
    public class MatchFormVM
    {
        [Required(ErrorMessage = "Förnamn krävs")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Namnet måste vara minst 2 bokstäver, max 50.")]
        public string Player1FirstName { get; set; }

        [Required(ErrorMessage = "Efternamn krävs")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Namnet måste vara minst 2 bokstäver, max 50.")]
        public string Player1LastName { get; set; }

        [Required(ErrorMessage = "Förnamn krävs")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Namnet måste vara minst 2 bokstäver, max 50.")]
        public string Player2FirstName { get; set; }

        [Required(ErrorMessage = "Efternamn krävs")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Namnet måste vara minst 2 bokstäver, max 50.")]
        public string Player2LastName { get; set; }

        [Required(ErrorMessage = "Ålder krävs")]
        [Range(1, 100, ErrorMessage = "Åldern måste vara mellan 1 och 100.")]
        public int Player1Age { get; set; }  //"int?" is needed for correct error message here. dont know how to make it work. 

        [Required(ErrorMessage = "Ålder krävs")]
        [Range(1, 100, ErrorMessage = "Åldern måste vara mellan 1 och 100.")]
        public int Player2Age { get; set; }
    }
}
