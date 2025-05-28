using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Match.Interface;
using TableTennis.ViewModels;

namespace TableTennis.Pages.Game.Statistics
{
    [BindProperties]
    public class statisticsModel : PageModel
    {
        private readonly IMatchService _matchService;
        public statisticsModel(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public List<SelectListItem> AvailablePlayers { get; set; } = new List<SelectListItem>();
        public List<PlayerInfoVM> AllPlayerDetailsForJs { get; set; } = new List<PlayerInfoVM>();
        public string? SelectedPlayer1Id { get; set; }
        public string? SelectedPlayer2Id { get; set; }
        public StatisticsVM Player1VM { get; set; }
        public StatisticsVM Player2VM { get; set; }

        public void OnGet()
        {
            PopulateAvailablePlayers();
        }

        public IActionResult OnPostPlayer1()
        {
            PopulateAvailablePlayers();

            var player1StatsDTO = _matchService.GetStats(SelectedPlayer1Id);
            StatisticsVM player1Stats = new StatisticsVM
            {
                PlayerFullName = player1StatsDTO.PlayerFullName,
                Wins = player1StatsDTO.Wins,
                Losses = player1StatsDTO.Losses,
                TotalGamesPlayed = player1StatsDTO.TotalGamesPlayed,
                WinPercentage = player1StatsDTO.WinPercentage,
                LongestMatch = player1StatsDTO.LongestMatch,
                FastestMatch = player1StatsDTO.FastestMatch,
                BestAgainstName = player1StatsDTO.BestAgainstName,
                BestAgainstWinRate = player1StatsDTO.BestAgainstWinRate,
                WorstAgainstName = player1StatsDTO.WorstAgainstName,
                WorstAgainstWinRate = player1StatsDTO.WorstAgainstWinRate

            };
            Player1VM = player1Stats;

            return Page();
        }

        private void PopulateAvailablePlayers()
        {
            var playerDTOs = _matchService.GetDistinctPlayers();
            AllPlayerDetailsForJs = new List<PlayerInfoVM>();
            AvailablePlayers.Clear();
            AvailablePlayers.Add(new SelectListItem("--- Välj befintlig spelare ---", ""));
            if (playerDTOs != null)
            {
                foreach (var dto in playerDTOs)
                {
                    var vm = new PlayerInfoVM
                    {
                        Id = dto.Id,
                        DisplayName = dto.DisplayName,
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Age = dto.Age
                    };
                    AllPlayerDetailsForJs.Add(vm);
                    AvailablePlayers.Add(new SelectListItem(vm.DisplayName, vm.Id?.ToString() ?? ""));
                }
            }
        }
    }
}
