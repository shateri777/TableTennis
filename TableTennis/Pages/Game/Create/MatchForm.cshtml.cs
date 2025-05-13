using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Match.Interface;

namespace TableTennis.Pages.Game.Create
{
    public class MatchFormModel : PageModel
    {
        private readonly IMatchService _matchService;

        public MatchFormModel(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public string Player1FirstName { get; set; }
        public string Player1LastName { get; set; }
        public string Player2FirstName { get; set; }
        public string Player2LastName { get; set; }

        public int Player1Age { get; set; }
        public int Player2Age { get; set; }


        public void OnGet()
        {
        }
    }
}
