using DataAccessLayer.Data.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pingis.ViewModels;
using Services.Match.Interface;
using TableTennis.ViewModels;

namespace TableTennis.Pages.Game.Create
{
    [BindProperties]
    public class MatchFormModel : PageModel
    {
        private readonly IMatchService _matchService;

        public MatchFormModel(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public MatchFormVM FormVM { get; set; }

        public int ChoosenSet { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {

            if(ModelState.IsValid)
            {
                var newMatch = new MatchDTO
                {
                    Player1FirstName = FormVM.Player1FirstName,
                    Player1LastName = FormVM.Player1LastName,
                    Player2FirstName = FormVM.Player2FirstName,
                    Player2LastName = FormVM.Player2LastName,
                    Player1Age = FormVM.Player1Age,
                    Player2Age = FormVM.Player2Age,
                    SetGender = ChoosenSet.ToString(),
                    MatchDate = DateTime.Now
                };
                _matchService.CreateMatch(newMatch);

                return RedirectToPage("/Game/Create/TableTennisMatch");
            }
            return Page();
        }
    }
}
