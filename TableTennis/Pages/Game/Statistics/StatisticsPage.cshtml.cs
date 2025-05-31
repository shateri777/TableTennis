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
        public PlayerComparisonVM ComparisonVM { get; set; }
        public PlayerComparisonVM ReverseComparisonVM { get; set; }


        public void OnGet()
        {
            PopulateAvailablePlayers();
        }

        public IActionResult OnPostPlayer1()
        {
            PopulateAvailablePlayers();

            if (!string.IsNullOrEmpty(SelectedPlayer1Id))
            {
                var dto1 = _matchService.GetStats(SelectedPlayer1Id);
                if (dto1 != null)
                {
                    Player1VM = new StatisticsVM
                    {
                        PlayerFullName = dto1.PlayerFullName,
                        Wins = dto1.Wins,
                        Losses = dto1.Losses,
                        TotalGamesPlayed = dto1.TotalGamesPlayed,
                        WinPercentage = dto1.WinPercentage,
                        LongestMatch = dto1.LongestMatch,
                        FastestMatch = dto1.FastestMatch,
                        BestAgainstName = dto1.BestAgainstName,
                        BestAgainstWinRate = dto1.BestAgainstWinRate,
                        WorstAgainstName = dto1.WorstAgainstName,
                        WorstAgainstWinRate = dto1.WorstAgainstWinRate
                    };
                }
            }

            if (!string.IsNullOrEmpty(SelectedPlayer2Id))
            {
                var dto2 = _matchService.GetStats(SelectedPlayer2Id);
                if (dto2 != null)
                {
                    Player2VM = new StatisticsVM
                    {
                        PlayerFullName = dto2.PlayerFullName,
                        Wins = dto2.Wins,
                        Losses = dto2.Losses,
                        TotalGamesPlayed = dto2.TotalGamesPlayed,
                        WinPercentage = dto2.WinPercentage,
                        LongestMatch = dto2.LongestMatch,
                        FastestMatch = dto2.FastestMatch,
                        BestAgainstName = dto2.BestAgainstName,
                        BestAgainstWinRate = dto2.BestAgainstWinRate,
                        WorstAgainstName = dto2.WorstAgainstName,
                        WorstAgainstWinRate = dto2.WorstAgainstWinRate
                    };
                }
            }

            if (!string.IsNullOrEmpty(SelectedPlayer1Id) && !string.IsNullOrEmpty(SelectedPlayer2Id))
            {
                var comparisonDTO = _matchService.ComparePlayers(SelectedPlayer1Id, SelectedPlayer2Id);
                if (comparisonDTO != null)
                {
                    ComparisonVM = new PlayerComparisonVM
                    {
                        Player1Name = comparisonDTO.Player1Name,
                        Player2Name = comparisonDTO.Player2Name,
                        TotalMatches = comparisonDTO.TotalMatches,
                        Player1Wins = comparisonDTO.Player1Wins,
                        Player2Wins = comparisonDTO.Player2Wins,
                        Player1WinRate = comparisonDTO.Player1WinRate,
                        Player2WinRate = comparisonDTO.Player2WinRate
                    };

                    ReverseComparisonVM = new PlayerComparisonVM
                    {
                        Player1Name = comparisonDTO.Player2Name,
                        Player2Name = comparisonDTO.Player1Name,
                        TotalMatches = comparisonDTO.TotalMatches,
                        Player1Wins = comparisonDTO.Player2Wins,
                        Player2Wins = comparisonDTO.Player1Wins,
                        Player1WinRate = comparisonDTO.Player2WinRate,
                        Player2WinRate = comparisonDTO.Player1WinRate
                    };
                }
            }

            return Page();
        }


        //public IActionResult OnPostPlayer1()
        //{
        //    PopulateAvailablePlayers();

        //    var player1StatsDTO = _matchService.GetStats(SelectedPlayer1Id);
        //    StatisticsVM player1Stats = new StatisticsVM
        //    {
        //        PlayerFullName = player1StatsDTO.PlayerFullName,
        //        Wins = player1StatsDTO.Wins,
        //        Losses = player1StatsDTO.Losses,
        //        TotalGamesPlayed = player1StatsDTO.TotalGamesPlayed,
        //        WinPercentage = player1StatsDTO.WinPercentage,
        //        LongestMatch = player1StatsDTO.LongestMatch,
        //        FastestMatch = player1StatsDTO.FastestMatch,
        //        BestAgainstName = player1StatsDTO.BestAgainstName,
        //        BestAgainstWinRate = player1StatsDTO.BestAgainstWinRate,
        //        WorstAgainstName = player1StatsDTO.WorstAgainstName,
        //        WorstAgainstWinRate = player1StatsDTO.WorstAgainstWinRate

        //    };
        //    Player1VM = player1Stats;

        //    return Page();
        //}

        public IActionResult OnPostPlayer2()
        {
            PopulateAvailablePlayers();

            if (!string.IsNullOrEmpty(SelectedPlayer2Id))
            {
                var dto2 = _matchService.GetStats(SelectedPlayer2Id);
                if (dto2 != null)
                {
                    Player2VM = new StatisticsVM
                    {
                        PlayerFullName = dto2.PlayerFullName,
                        Wins = dto2.Wins,
                        Losses = dto2.Losses,
                        TotalGamesPlayed = dto2.TotalGamesPlayed,
                        WinPercentage = dto2.WinPercentage,
                        LongestMatch = dto2.LongestMatch,
                        FastestMatch = dto2.FastestMatch,
                        BestAgainstName = dto2.BestAgainstName,
                        BestAgainstWinRate = dto2.BestAgainstWinRate,
                        WorstAgainstName = dto2.WorstAgainstName,
                        WorstAgainstWinRate = dto2.WorstAgainstWinRate
                    };
                }
            }

            if (!string.IsNullOrEmpty(SelectedPlayer1Id))
            {
                var dto1 = _matchService.GetStats(SelectedPlayer1Id);
                if (dto1 != null)
                {
                    Player1VM = new StatisticsVM
                    {
                        PlayerFullName = dto1.PlayerFullName,
                        Wins = dto1.Wins,
                        Losses = dto1.Losses,
                        TotalGamesPlayed = dto1.TotalGamesPlayed,
                        WinPercentage = dto1.WinPercentage,
                        LongestMatch = dto1.LongestMatch,
                        FastestMatch = dto1.FastestMatch,
                        BestAgainstName = dto1.BestAgainstName,
                        BestAgainstWinRate = dto1.BestAgainstWinRate,
                        WorstAgainstName = dto1.WorstAgainstName,
                        WorstAgainstWinRate = dto1.WorstAgainstWinRate
                    };
                }
            }

            if (!string.IsNullOrEmpty(SelectedPlayer1Id) && !string.IsNullOrEmpty(SelectedPlayer2Id))
            {
                var comparisonDTO = _matchService.ComparePlayers(SelectedPlayer1Id, SelectedPlayer2Id);
                if (comparisonDTO != null)
                {
                    ComparisonVM = new PlayerComparisonVM
                    {
                        Player1Name = comparisonDTO.Player1Name,
                        Player2Name = comparisonDTO.Player2Name,
                        TotalMatches = comparisonDTO.TotalMatches,
                        Player1Wins = comparisonDTO.Player1Wins,
                        Player2Wins = comparisonDTO.Player2Wins,
                        Player1WinRate = comparisonDTO.Player1WinRate,
                        Player2WinRate = comparisonDTO.Player2WinRate
                    };

                    ReverseComparisonVM = new PlayerComparisonVM
                    {
                        Player1Name = comparisonDTO.Player2Name,
                        Player2Name = comparisonDTO.Player1Name,
                        TotalMatches = comparisonDTO.TotalMatches,
                        Player1Wins = comparisonDTO.Player2Wins,
                        Player2Wins = comparisonDTO.Player1Wins,
                        Player1WinRate = comparisonDTO.Player2WinRate,
                        Player2WinRate = comparisonDTO.Player1WinRate
                    };
                }
            }

            return Page();
        }



        //public IActionResult OnPostPlayer2()
        //{
        //    PopulateAvailablePlayers();

        //    var player2StatsDTO = _matchService.GetStats(SelectedPlayer2Id);
        //    if (player2StatsDTO == null)
        //    {
        //        Player2VM = new StatisticsVM(); // tom fallback
        //        return Page();
        //    }

        //    Player2VM = new StatisticsVM
        //    {
        //        PlayerFullName = player2StatsDTO.PlayerFullName,
        //        Wins = player2StatsDTO.Wins,
        //        Losses = player2StatsDTO.Losses,
        //        TotalGamesPlayed = player2StatsDTO.TotalGamesPlayed,
        //        WinPercentage = player2StatsDTO.WinPercentage,
        //        LongestMatch = player2StatsDTO.LongestMatch,
        //        FastestMatch = player2StatsDTO.FastestMatch,
        //        BestAgainstName = player2StatsDTO.BestAgainstName,
        //        BestAgainstWinRate = player2StatsDTO.BestAgainstWinRate,
        //        WorstAgainstName = player2StatsDTO.WorstAgainstName,
        //        WorstAgainstWinRate = player2StatsDTO.WorstAgainstWinRate
        //    };

        //    return Page();
        //}

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
                    var idStr = dto.Id?.ToString() ?? "";
                    var vm = new PlayerInfoVM
                    {
                        Id = dto.Id,
                        DisplayName = dto.DisplayName,
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Age = dto.Age
                    };

                    AllPlayerDetailsForJs.Add(vm);

                    // Filtrera bort spelare som redan valts i andra dropdownen
                    bool skip = false;
                    if (!string.IsNullOrEmpty(SelectedPlayer1Id) && SelectedPlayer1Id == idStr)
                        skip = true;

                    if (!string.IsNullOrEmpty(SelectedPlayer2Id) && SelectedPlayer2Id == idStr)
                        skip = true;

                    if (!skip)
                    {
                        AvailablePlayers.Add(new SelectListItem(vm.DisplayName, idStr));
                    }
                }
            }
        }

    }
}
