using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TableTennis.ViewModels;
using Services.Match.Interface;
using Microsoft.AspNetCore.Mvc;

namespace TableTennis.Pages.Game.ReadAll
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IMatchService _matchService;
        private readonly ISetService _setService;
        public IndexModel(IMatchService matchService, ISetService setService)
        {
            _matchService = matchService;
            _setService = setService;
        }
        public IList<MatchHistoryVM> MatchHistories { get; set; } = new List<MatchHistoryVM>();
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public void OnGet()
        {
            var allMatchDTOs = _matchService.GetAllMatches(SearchTerm);
            if (allMatchDTOs != null)
            {
                foreach (var matchDto in allMatchDTOs)
                {
                    int player1SetsWon = _setService.GetSetsWonByPlayerName(matchDto.Id, matchDto.Player1FirstName);
                    int player2SetsWon = _setService.GetSetsWonByPlayerName(matchDto.Id, matchDto.Player2FirstName);
                    var matchHistoryVm = new MatchHistoryVM
                    {
                        Player1FullName = $"{matchDto.Player1FirstName} {matchDto.Player1LastName}",
                        Player2FullName = $"{matchDto.Player2FirstName} {matchDto.Player2LastName}",
                        Player1Age = matchDto.Player1Age,
                        Player2Age = matchDto.Player2Age,
                        SetGender = matchDto.SetGender,
                        WinnerPlayer = matchDto.WinnerPlayer,
                        Player1Score = player1SetsWon,
                        Player2Score = player2SetsWon, 
                        MatchDate = matchDto.MatchDate,
                        BestOfSets = matchDto.BestOfSets
                    };
                    MatchHistories.Add(matchHistoryVm);
                }
            }
        }
    }
}
