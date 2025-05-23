using DataAccessLayer.Data.DTO;
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
            MatchFormVM = new MatchFormVM();
            SetVM = new SetVM();
        }

        public int MatchId { get; set; }
        public SetVM SetVM { get; set; }
        public MatchFormVM MatchFormVM { get; set; }
        public int SetCounter { get; set; } = 1;

        public IActionResult OnGet(int matchId)
        {
            MatchId = matchId;
            var matchDto = _matchService.FindMatchId(matchId);
            if (matchDto == null)
                return RedirectToPage("/Error");

            MatchFormVM = new MatchFormVM
            {
                Player1FirstName = matchDto.Player1FirstName,
                Player1LastName = matchDto.Player1LastName,
                Player2FirstName = matchDto.Player2FirstName,
                Player2LastName = matchDto.Player2LastName,
                Player1Age = matchDto.Player1Age,
                Player2Age = matchDto.Player2Age,
                BestOfSets = matchDto.BestOfSets,
                WinnerPlayer = matchDto.WinnerPlayer,
            };

            // Hämta aktivt set eller skapa ett nytt om det saknas
            var set = _setService.GetActiveSetId(matchId);
            if (set == null)
            {
                _setService.CreateSet(matchId);
                set = _setService.GetActiveSetId(matchId);
            }
            SetVM = new SetVM
            {
                MatchId = matchId,
                Player1Score = set.Player1Score,
                Player2Score = set.Player2Score,
                WinnerPlayer = set.WinnerPlayer,
                ServeCounter = set.ServeCounter,
                IsPlayer1Serve = set.IsPlayer1Serve,
            };

            SetCounter = _setService.GetSetCount(matchId);

            // Räkna vunna set och kolla om någon har vunnit matchen
            int setsToWin = (matchDto.BestOfSets / 2) + 1;
            int p1Sets = _setService.GetSetsWonByPlayerName(matchId, "Player1");
            int p2Sets = _setService.GetSetsWonByPlayerName(matchId, "Player2");
            if (p1Sets >= setsToWin)
            {
                MatchFormVM.WinnerPlayer = matchDto.Player1FirstName;
            }
            else if (p2Sets >= setsToWin)
            {
                MatchFormVM.WinnerPlayer = matchDto.Player2FirstName;
            }

            return Page();
        }

        public IActionResult OnPostAddPointToPlayer1(int matchId)
        {
            var set = _setService.GetActiveSetId(matchId);
            if (set?.WinnerPlayer == null)
            {
                _setService.AddPointToPlayer1(matchId);
            }
            return RedirectToPage(new { matchId });
        }

        public IActionResult OnPostRemovePointFromPlayer1(int matchId)
        {
            var set = _setService.GetActiveSetId(matchId);
            if (set?.WinnerPlayer == null)
            {
                _setService.ChangePlayer1Score(matchId, -1);
            }
            return RedirectToPage(new { matchId });
        }

        public IActionResult OnPostAddPointToPlayer2(int matchId)
        {
            var set = _setService.GetActiveSetId(matchId);
            if (set?.WinnerPlayer == null)
            {
                _setService.AddPointToPlayer2(matchId);
            }
            return RedirectToPage(new { matchId });
        }

        public IActionResult OnPostRemovePointFromPlayer2(int matchId)
        {
            var set = _setService.GetActiveSetId(matchId);
            if (set?.WinnerPlayer == null)
            {
                _setService.ChangePlayer2Score(matchId, -1);
            }
            return RedirectToPage(new { matchId });
        }

        public IActionResult OnPostContinueSet(int matchId)
        {
            ModelState.Clear();
            var matchDto = _matchService.FindMatchId(matchId);
            if (matchDto == null)
                return RedirectToPage("/Error");

            // Kolla om någon redan har vunnit matchen
            int setsToWin = (matchDto.BestOfSets / 2) + 1;
            int p1Sets = _setService.GetSetsWonByPlayerName(matchId, "Player1");
            int p2Sets = _setService.GetSetsWonByPlayerName(matchId, "Player2");
            if (p1Sets >= setsToWin || p2Sets >= setsToWin)
            {
                // Ingen fler set ska startas, matchen är över
                return RedirectToPage(new { matchId });
            }

            _setService.CreateSet(matchId);
            SetCounter = _setService.GetSetCount(matchId);
            return RedirectToPage(new { matchId });
        }
    }
}
