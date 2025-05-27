using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Match.Interface;
using Services.Player.Interface;
using TableTennis.ViewModels;

namespace TableTennis.Pages.Game.Leaderboard
{
    public class LeaderboardPageModel : PageModel
    {
        private readonly IMatchService _matchService;
        private readonly IPlayerService _playerService;
        public LeaderboardPageModel(IMatchService matchService, IPlayerService playerService)
        {
            _matchService = matchService;
            _playerService = playerService;
        }
        public List<LeaderboardEntryVM> LeaderboardEntries { get; set; }
        public void OnGet()
        {
            var allMatches = _matchService.GetAllMatches();
            var playerStatsDict = _playerService.GetTopPlayers(allMatches);
            const int minimumGamesPlayedForHighRank = 10;
            LeaderboardEntries = playerStatsDict.Values
                .OrderByDescending(ps => ps.TotalGamesPlayed >= minimumGamesPlayedForHighRank ? ps.WinPercentage : -1)
                .ThenByDescending(ps => ps.WinPercentage) 
                .ThenByDescending(ps => ps.Wins)        
                .ThenBy(ps => ps.TotalGamesPlayed)            
                .Take(10)
                .Select((ps, index) => new LeaderboardEntryVM
                {
                    Rank = index + 1,
                    PlayerFullName = ps.PlayerFullName,
                    Wins = ps.Wins,
                    TotalGamesPlayed = ps.TotalGamesPlayed,
                    WinPercentage = ps.WinPercentage,
                })
                .ToList();

        }
    }
}
