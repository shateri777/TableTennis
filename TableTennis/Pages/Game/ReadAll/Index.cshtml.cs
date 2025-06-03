using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Match.Interface;
using TableTennis.ViewModels;

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
        public IList<MatchHistoryVM> InactiveMatchHistories { get; set; } = new List<MatchHistoryVM>();
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty]
        public int SelectedInactiveMatchId { get; set; }
        public void OnGet()
        {
            var allMatchDTOs = _matchService.GetAllMatches(SearchTerm);
            if (allMatchDTOs != null)
            {
                foreach (var matchDto in allMatchDTOs)
                {
                    var setsForMatch = _setService.GetSetsForMatch(matchDto.Id);
                    List<string> setScoreDetailsList = new List<string>();
                    if (setsForMatch != null)
                    {
                        setScoreDetailsList = setsForMatch
                            .Where(s => s.WinnerPlayer != null) // Inkludera bara avslutade set
                            .OrderBy(s => s.Id)          // Antag att TableTennisSet har SetNumber
                            .Select((s, index) => $"Set {index + 1}: {s.Player1Score}-{s.Player2Score}") // Player1Score/Player2Score är poäng här
                            .ToList();
                    }
                    int player1SetsWon = _setService.GetSetsWonByPlayerName(matchDto.Id, matchDto.Player1FirstName);
                    int player2SetsWon = _setService.GetSetsWonByPlayerName(matchDto.Id, matchDto.Player2FirstName);
                    var matchHistoryVm = new MatchHistoryVM
                    {
                        Id = matchDto.Id,
                        Player1FullName = $"{matchDto.Player1FirstName} {matchDto.Player1LastName}",
                        Player2FullName = $"{matchDto.Player2FirstName} {matchDto.Player2LastName}",
                        Player1Age = matchDto.Player1Age,
                        Player2Age = matchDto.Player2Age,
                        SetGender = matchDto.SetGender,
                        Player1Score = player1SetsWon,
                        Player2Score = player2SetsWon,
                        MatchDate = matchDto.MatchDate,
                        BestOfSets = matchDto.BestOfSets,
                        WinnerPlayer = matchDto.WinnerPlayer,
                        IsPlayer1Winner = !string.IsNullOrEmpty(matchDto.WinnerPlayer) && matchDto.WinnerPlayer.Equals(matchDto.Player1FirstName, StringComparison.OrdinalIgnoreCase),
                        IsPlayer2Winner = !string.IsNullOrEmpty(matchDto.WinnerPlayer) && matchDto.WinnerPlayer.Equals(matchDto.Player2FirstName, StringComparison.OrdinalIgnoreCase),
                        SetScoresDetails = setScoreDetailsList,
                    };
                    MatchHistories.Add(matchHistoryVm);
                }
            }
            var inactiveMatchDTOs = _matchService.GetAllInactiveMatches();
            InactiveMatchHistories.Clear();
            if (inactiveMatchDTOs != null)
            {
                foreach (var matchDto in inactiveMatchDTOs)
                {
                    InactiveMatchHistories.Add(new MatchHistoryVM
                    {
                        Id = matchDto.Id,
                        DisplayText = $"ID: {matchDto.Id} - {matchDto.Player1FirstName} {matchDto.Player1LastName} vs {matchDto.Player2FirstName} {matchDto.Player2LastName} ({matchDto.MatchDate:yyyy-MM-dd})"
                    });
                }
            }
        }
        public IActionResult OnPostSoftDelete(int matchId)
        {
            if (matchId <= 0)
            {
                return NotFound();
            }
            var matchToDelete = _matchService.FindMatchId(matchId);
            if (matchToDelete != null)
            {
                _matchService.SoftDeleteMatch(matchToDelete);
                TempData["StatusMessage"] = $"Matchen med ID {matchId} har tagits bort.";
            }
            else
            {
                TempData["ErrorMessage"] = $"Kunde inte hitta matchen med ID {matchId}.";
            }
            return RedirectToPage();
        }
        public IActionResult OnPostRestore()
        {
            if (SelectedInactiveMatchId <= 0)
            {
                TempData["ErrorMessage"] = "Inget giltigt match-ID valdes för återställning.";
                return RedirectToPage();
            }

            if (SelectedInactiveMatchId > 0)
            {
                _matchService.RestoreDeletedMatch(SelectedInactiveMatchId);
                TempData["StatusMessage"] = $"Matchen med ID {SelectedInactiveMatchId} har återställts.";
            }
            else
            {
                TempData["ErrorMessage"] = $"Kunde inte hitta eller återställa matchen med ID {SelectedInactiveMatchId}.";
            }
            return RedirectToPage();
        }
    }
}
