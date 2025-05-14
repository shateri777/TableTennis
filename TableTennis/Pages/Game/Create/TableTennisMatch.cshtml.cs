using DataAccessLayer.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pingis.ViewModels;
using Services.Match.Interface;
using TableTennis.ViewModels;

namespace TableTennis.Pages.Game.Create
{
    [BindProperties]
    public class TableTennisMatchModel : PageModel
    {

        private readonly IMatchService _matchService;

        public TableTennisMatchModel(IMatchService matchService)
        {
            _matchService = matchService;
        }




        public int Id { get; set; }
        public int MatchId { get; set; }

        public SetVM SetVM { get; set; }

        public MatchFormVM MatchFormVM { get; set; }




        public void OnGet(int matchId)
        {

            var match = _matchService.findMatchId(matchId);

            if (match == null)
            {
                // Gör något bättre än att krascha – kanske redirect eller felmeddelande?
                RedirectToPage("/Error"); // eller return NotFound();
                return;
            }


            MatchFormVM = new MatchFormVM
            {
                Player1FirstName = match.Player1FirstName,
                Player1LastName = match.Player1LastName,
                Player2FirstName = match.Player2FirstName,
                Player2LastName = match.Player2LastName,
                Player1Age = match.Player1Age,
                Player2Age = match.Player2Age
            };

        }


        //public IActionResult OnPostAddPointToPlayer1()
        //{
        //    // logik här
        //}

        //public IActionResult OnPostAddPointToPlayer2()
        //{
        //    // logik här
        //}

        //public IActionResult OnPostStartNewMatch()
        //{
        //    // starta ny match
        //}


    }
}
