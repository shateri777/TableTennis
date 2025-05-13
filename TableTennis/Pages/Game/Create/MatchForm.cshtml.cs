using DataAccessLayer.Data.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        //public string Player1FirstName { get; set; }
        //public string Player1LastName { get; set; }
        //public string Player2FirstName { get; set; }
        //public string Player2LastName { get; set; }

        //public int Player1Age { get; set; }
        //public int Player2Age { get; set; }

        public int ChoosenSet { get; set; }

        public void OnGet()
        {
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
                    SetGender = ChoosenSet.ToString(),
                    MatchDate = DateTime.Now
                };
                _matchService.CreateMatch(newMatch);
                //var newMatch = new SetsDTO
                //{
                //    Player1FirstName = Player1FirstName,
                //    Player1LastName = Player1LastName,
                //    Player2FirstName = Player2FirstName,
                //    Player2LastName = Player2LastName,
                //    Player1Age = Player1Age,
                //    Player2Age = Player2Age,
                //    SetGender = ChoosenSet.ToString(),
                //    MatchDate = DateTime.Now
                //};
                //_matchService.CreateMatch(new DataAccessLayer.Data.DTO.SetsDTO
                //{
                //    Player1FirstName = Player1FirstName,
                //    Player1LastName = Player1LastName,
                //    Player2FirstName = Player2FirstName,
                //    Player2LastName = Player2LastName,
                //    Player1Age = Player1Age,
                //    Player2Age = Player2Age,
                //    SetGender = ChoosenSet.ToString(),
                //    MatchDate = DateTime.Now
                //});
                return RedirectToPage("/Game/Create/TableTennisMatch");
            }
            return Page();
        }
    }
}
