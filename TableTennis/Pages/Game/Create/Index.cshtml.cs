using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Data.Models;
using TableTennis.Data;
using Services.Match.Interface;
using DataAccessLayer.Data.DTO;

namespace TableTennis.Pages.Game.Create
{
    public class IndexModel : PageModel
    {
        private readonly IMatchService _matchService;

        public IndexModel(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public string SetGender { get; set; }

        //public List<TableTennisSet> Sets { get; set; }

        //[BindProperty]
        //public SetsDTO CurrentSet { get; set; }
        //public SetsDTO DatabaseSet { get; set; }

        //[BindProperty]
        //public string Winner { get; set; }

        //public void OnGet()
        //{
        //    // Initialisera en ny match om det är första gången sidan besöks
        //    if (CurrentSet == null)
        //    {
        //        StartNewSet();
        //    }
        //}

        //public IActionResult OnPostAddPointToPlayer1()
        //{
        //    DatabaseSet = _matchService.AddPointToPlayer1(CurrentSet.Id);
        //    UpdateGame();
        //    return Page();
        //}

        //public IActionResult OnPostAddPointToPlayer2()
        //{
        //    DatabaseSet = _matchService.AddPointToPlayer2(CurrentSet.Id);
        //    UpdateGame();
        //    return Page();
        //}

        //public IActionResult OnPostStartNewGame()
        //{
        //    StartNewSet();
        //    return Page();
        //}

        //private void StartNewSet()
        //{
        //    // Rensa ModelState för att undvika att gamla värden återkommer
        //    ModelState.Clear();

        //    // Skapa ett nytt set och lägg till det i databasen
        //    CurrentSet = new SetsDTO();
        //    _matchService.CreateSet(CurrentSet);

        //    // Återställ Winner till null för nästa omgång
        //    Winner = null;
        //}

        //private void UpdateGame()
        //{
        //    Winner = _matchService.CheckEndOfSet(CurrentSet.Id);
        //    _matchService.UpdateServe(CurrentSet.Id);

        //    if (!string.IsNullOrEmpty(Winner))
        //    {
        //        // Stoppa spelet och vänta på att användaren startar ett nytt spel manuellt
        //    }
        //    else
        //    {
        //        CurrentSet = DatabaseSet;
        //    }
        //}
    }
}
