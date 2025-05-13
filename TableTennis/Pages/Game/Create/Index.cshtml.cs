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
        //    // Initialisera en ny match om det �r f�rsta g�ngen sidan bes�ks
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
        //    // Rensa ModelState f�r att undvika att gamla v�rden �terkommer
        //    ModelState.Clear();

        //    // Skapa ett nytt set och l�gg till det i databasen
        //    CurrentSet = new SetsDTO();
        //    _matchService.CreateSet(CurrentSet);

        //    // �terst�ll Winner till null f�r n�sta omg�ng
        //    Winner = null;
        //}

        //private void UpdateGame()
        //{
        //    Winner = _matchService.CheckEndOfSet(CurrentSet.Id);
        //    _matchService.UpdateServe(CurrentSet.Id);

        //    if (!string.IsNullOrEmpty(Winner))
        //    {
        //        // Stoppa spelet och v�nta p� att anv�ndaren startar ett nytt spel manuellt
        //    }
        //    else
        //    {
        //        CurrentSet = DatabaseSet;
        //    }
        //}
    }
}
