using DataAccessLayer.Data.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pingis.ViewModels;
using Services.Match.Interface;
using TableTennis.ViewModels;

namespace TableTennis.Pages.Game.Create
{
    [BindProperties]
    public class MatchFormModel : PageModel
    {
        private readonly IMatchService _matchService;

        public MatchFormModel(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public MatchFormVM FormVM { get; set; }
        public string SelectedPlayer1Id { get; set; }
        public string SelectedPlayer2Id { get; set; }
        public List<SelectListItem> AvailablePlayers { get; set; } = new List<SelectListItem>();
        public List<PlayerInfoVM> AllPlayerDetailsForJs { get; set; } = new List<PlayerInfoVM>();
        public void OnGet(string gender)
        {
            FormVM = new MatchFormVM();
            FormVM.SetGender = gender;
            PopulateAvailablePlayers();
        }


        public IActionResult OnPost()
        {

            if(ModelState.IsValid)
            {
                var newMatch = new MatchDTO
                {
                    Player1FirstName = FormVM.Player1FirstName,
                    Player1LastName = FormVM.Player1LastName,
                    Player2FirstName = FormVM.Player2FirstName,
                    Player2LastName = FormVM.Player2LastName,
                    Player1Age = FormVM.Player1Age,
                    Player2Age = FormVM.Player2Age,
                    SetGender = FormVM.SetGender,
                    BestOfSets = FormVM.BestOfSets,
                    MatchDate = DateTime.Now
                };
                if (!string.IsNullOrEmpty(newMatch.Player1FirstName) &&
                !string.IsNullOrEmpty(newMatch.Player2FirstName) &&
                newMatch.Player1FirstName.Equals(newMatch.Player2FirstName, StringComparison.OrdinalIgnoreCase) &&
                newMatch.Player1LastName.Equals(newMatch.Player2LastName, StringComparison.OrdinalIgnoreCase) &&
                newMatch.Player1Age == newMatch.Player2Age)
                {
                    ModelState.AddModelError(string.Empty, "Spelare 1 och Spelare 2 kan inte vara samma person med samma namn och ålder.");
                    PopulateAvailablePlayers(); // Ladda om dropdowns om validering misslyckas
                    return Page();
                }
                int createdMatchId = _matchService.CreateMatch(newMatch);
                return RedirectToPage("/Game/Create/TableTennisMatch", new { matchId = createdMatchId });

            }
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
                    AvailablePlayers.Add(new SelectListItem(vm.DisplayName, vm.Id));
                }
            }
        }
    }
}
