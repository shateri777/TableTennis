using DataAccessLayer.Data.DTO;
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
        private readonly ISetService _setService;

        public TableTennisMatchModel(IMatchService matchService, ISetService setService)
        {
            _matchService = matchService;
            _setService = setService;
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
                RedirectToPage("/Error");
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


            MatchId = matchId;


            SetVM = new SetVM
            {
                MatchId = matchId
            };

            _setService.CreateSet(MatchId);
        }

        public IActionResult OnPostAddPointToPlayer1(int matchId)
        {

            var match = _matchService.findMatchId(matchId);

            if (match == null)
            {
                RedirectToPage("/Error");
                return Page();
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

            SetVM.Player1Score = _setService.GetPlayer1Score(matchId);
            SetVM.Player2Score = _setService.GetPlayer2Score(matchId);

            MatchId = matchId;
            SetVM.Player1Score = _setService.AddPointToPlayer1(matchId);

            return Page();
        }

        public IActionResult OnPostAddPointToPlayer2(int matchId)
        {
            var match = _matchService.findMatchId(matchId);

            if (match == null)
            {
                RedirectToPage("/Error");
                return Page();
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

            SetVM.Player1Score = _setService.GetPlayer1Score(matchId);
            SetVM.Player2Score = _setService.GetPlayer2Score(matchId);

            MatchId = matchId;
            SetVM.Player2Score = _setService.AddPointToPlayer2(matchId);


            return Page();
        }

        //public void AddPointToPlayer1()
        //{
        //    Player1Score++;
        //    CheckEndOfSet();
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
