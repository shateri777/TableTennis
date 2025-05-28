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

        public void OnGet()
        {
            PopulateAvailablePlayers();
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
