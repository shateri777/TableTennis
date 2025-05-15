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
        public int SetCounter { get; set; } = 1;


        public void OnGet(int matchId)
        {

            var match = _matchService.FindMatchId(matchId);

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
                Player2Age = match.Player2Age,
                BestOfSets = match.BestOfSets,
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

            var match = _matchService.FindMatchId(matchId);

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
                Player2Age = match.Player2Age,
                BestOfSets = match.BestOfSets,
            };

            SetVM.Player1Score = _setService.GetPlayer1Score(matchId);
            SetVM.Player2Score = _setService.GetPlayer2Score(matchId);

            MatchId = matchId;
            SetVM.Player1Score = _setService.AddPointToPlayer1(matchId);

            var endOfset =_setService.CheckEndOfSet(matchId);

            if (endOfset == "Player1")
            {
                SetVM.WinnerPlayer = match.Player1FirstName;
                _setService.SetWinnerPlayer(matchId, match.Player1FirstName);
                SetCounter++;
            }
            else if (endOfset == "Player2")
            {
                SetVM.WinnerPlayer = match.Player2FirstName;
                _setService.SetWinnerPlayer(matchId, match.Player1FirstName);
                SetCounter++;
            }
            else
            {
                SetVM.WinnerPlayer = null;
                var serve = _setService.UpdateServe(matchId);

                if (serve)
                {
                    SetVM.IsPlayer1Serve = !SetVM.IsPlayer1Serve;
                }
            }

            return Page();
        }

        public IActionResult OnPostAddPointToPlayer2(int matchId)
        {
            var match = _matchService.FindMatchId(matchId);

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
                Player2Age = match.Player2Age,
                BestOfSets = match.BestOfSets,
            };

            SetVM.Player1Score = _setService.GetPlayer1Score(matchId);
            SetVM.Player2Score = _setService.GetPlayer2Score(matchId);

            MatchId = matchId;
            SetVM.Player2Score = _setService.AddPointToPlayer2(matchId);

            var endOfset = _setService.CheckEndOfSet(matchId);

            if (endOfset == "Player1")
            {
                SetVM.WinnerPlayer = match.Player1FirstName;
                _setService.SetWinnerPlayer(matchId, match.Player1FirstName);
                SetCounter++;
            }
            else if (endOfset == "Player2")
            {
                SetVM.WinnerPlayer = match.Player2FirstName;
                _setService.SetWinnerPlayer(matchId, match.Player1FirstName);
                SetCounter++;
            }
            else
            {
                SetVM.WinnerPlayer = null;
                var serve = _setService.UpdateServe(matchId);

                if (serve)
                {
                    SetVM.IsPlayer1Serve = !SetVM.IsPlayer1Serve;
                }
            }

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

        public IActionResult OnPostContinueSet(int matchId)
        {
            ModelState.Clear();
            var match = _matchService.FindMatchId(matchId);

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
                Player2Age = match.Player2Age,
                BestOfSets = match.BestOfSets,
            };
            _setService.CreateSet(matchId);
            var set = _setService.GetActiveSetId(matchId);
            SetVM = new SetVM
            {
                Player1Score = set.Player1Score,
                Player2Score = set.Player2Score,
                WinnerPlayer = set.WinnerPlayer,
                ServeCounter = set.ServeCounter,
                IsPlayer1Serve = set.IsPlayer1Serve,
            };
            //SetVM = new SetVM
            //{
            //    Player1Score = 0,
            //    Player2Score = 0,
            //    WinnerPlayer = null
            //};
            return Page();
        }
    }
}
