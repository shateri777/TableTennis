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

        public void OnGet(string gender)
        {
            FormVM = new MatchFormVM();
            FormVM.SetGender = gender;
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
                    SetGender = FormVM.SetGender,
                    BestOfSets = FormVM.BestOfSets,
                    MatchDate = DateTime.Now
                };


                int createdMatchId = _matchService.CreateMatch(newMatch);
                return RedirectToPage("/Game/Create/TableTennisMatch", new { matchId = createdMatchId });

            }
            return Page();
        }
    }
}
