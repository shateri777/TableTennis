using DataAccessLayer.Data.DTO;
using Services.Match.Interface;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace Services.Match
{
    public class MatchService : IMatchService
    {
        private readonly ApplicationDbContext _dbContext;
        public MatchService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int CreateMatch(MatchDTO matchDTO)
        {
            var match = new DataAccessLayer.Data.Models.TableTennisMatch
            {
                Player1FirstName = matchDTO.Player1FirstName,
                Player1LastName = matchDTO.Player1LastName,
                Player2FirstName = matchDTO.Player2FirstName,
                Player2LastName = matchDTO.Player2LastName,
                Player1Age = matchDTO.Player1Age,
                Player2Age = matchDTO.Player2Age,
                SetGender = matchDTO.SetGender,
                MatchDate = matchDTO.MatchDate,
                BestOfSets = matchDTO.BestOfSets,
                Player1WonSets = 0,
                Player2WonSets = 0,
            };
            _dbContext.Match.Add(match);
            _dbContext.SaveChanges();
            return match.Id;
        }
        public MatchDTO FindMatchId(int matchId)
        {
            var match = _dbContext.Match.FirstOrDefault(m => m.Id == matchId);
            if (match != null)
            {
                return new MatchDTO
                {
                    Id = match.Id,
                    Player1FirstName = match.Player1FirstName,
                    Player1LastName = match.Player1LastName,
                    Player2FirstName = match.Player2FirstName,
                    Player2LastName = match.Player2LastName,
                    Player1Age = match.Player1Age,
                    Player2Age = match.Player2Age,
                    BestOfSets = match.BestOfSets,
                    SetGender = match.SetGender,
                    MatchDate = match.MatchDate,
                    Player1WonSets = match.Player1WonSets,
                    Player2WonSets = match.Player2WonSets,
                };
            }
            return null;
        }
        public string CheckMatchWinner(int matchId)
        {
            var match = _dbContext.Match.FirstOrDefault(m => m.Id == matchId);
            var completedSets = _dbContext.Sets
                                    .Where(s => s.MatchId == matchId && s.WinnerPlayer != null)
                                    .ToList();

            if (completedSets.Any())
            {
                var player1Wins = completedSets.Count(s => s.WinnerPlayer == match.Player1FirstName);
                var player2Wins = completedSets.Count(s => s.WinnerPlayer == match.Player2FirstName);
                int winsNeeded = (match.BestOfSets / 2) + 1;
                string determinedWinner = null;
                if (player1Wins >= winsNeeded)
                {
                    determinedWinner = match.Player1FirstName;
                }
                else if (player2Wins >= winsNeeded)
                {
                    determinedWinner = match.Player2FirstName;
                }
                if (determinedWinner != null)
                {
                    match.WinnerPlayer = determinedWinner;
                    match.TotalMatchTime = completedSets.Sum(s => s.SetTime);
                    _dbContext.Update(match); 
                    _dbContext.SaveChanges();
                    return determinedWinner;
                }
            }
            return null;
        }
        public List<MatchDTO> GetAllMatches(string searchTerm)
        {
            var query = _dbContext.Match.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var lowerSearchTerm = searchTerm.ToLower();
                query = query.Where(m =>
                    (m.Player1FirstName.ToLower() + " " + m.Player1LastName.ToLower()).Contains(lowerSearchTerm) ||
                    (m.Player2FirstName.ToLower() + " " + m.Player2LastName.ToLower()).Contains(lowerSearchTerm) ||
                    m.Player1FirstName.ToLower().Contains(lowerSearchTerm) ||
                    m.Player1LastName.ToLower().Contains(lowerSearchTerm) ||
                    m.Player2FirstName.ToLower().Contains(lowerSearchTerm) ||
                    m.Player2LastName.ToLower().Contains(lowerSearchTerm)
                );
            }
            return query
                .OrderByDescending(m => m.MatchDate)
                .Select(m => new MatchDTO
                {
                    Id = m.Id,
                    Player1FirstName = m.Player1FirstName,
                    Player1LastName = m.Player1LastName,
                    Player2FirstName = m.Player2FirstName,
                    Player2LastName = m.Player2LastName,
                    Player1Age = m.Player1Age,
                    Player2Age = m.Player2Age,
                    SetGender = m.SetGender,
                    MatchDate = m.MatchDate,
                    BestOfSets = m.BestOfSets,
                    WinnerPlayer = m.WinnerPlayer
                })
                .ToList();
        }
        public List<MatchDTO> GetAllMatches()
        {
            var allMatches = _dbContext.Match.Where(m => m.IsAcitve == true);
            return allMatches
                .OrderByDescending(m => m.MatchDate)
                .Select(m => new MatchDTO
                {
                    Id = m.Id,
                    Player1FirstName = m.Player1FirstName,
                    Player1LastName = m.Player1LastName,
                    Player2FirstName = m.Player2FirstName,
                    Player2LastName = m.Player2LastName,
                    Player1Age = m.Player1Age,
                    Player2Age = m.Player2Age,
                    SetGender = m.SetGender,
                    MatchDate = m.MatchDate,
                    BestOfSets = m.BestOfSets,
                    WinnerPlayer = m.WinnerPlayer
                })
                .ToList();
        }
        public List<PlayerInfoDTO> GetDistinctPlayers()
        {
            var player1s = _dbContext.Match
                .Where(m => m.Player1FirstName != null && m.Player1LastName != null)
                .Select(m => new { m.Player1FirstName, m.Player1LastName, m.Player1Age })
                .Distinct()
                .ToList();

            var player2s = _dbContext.Match
                .Where(m => m.Player2FirstName != null && m.Player2LastName != null)
                .Select(m => new { m.Player2FirstName, m.Player2LastName, m.Player2Age })
                .Distinct()
                .ToList();

            var allPlayers = new List<PlayerInfoDTO>();

            foreach (var p in player1s)
            {
                allPlayers.Add(new PlayerInfoDTO
                {
                    FirstName = p.Player1FirstName,
                    LastName = p.Player1LastName,
                    Age = p.Player1Age,
                    // Skapa ett unikt ID, t.ex. baserat på namn och ålder. Var försiktig med specialtecken.
                    Id = $"{p.Player1FirstName}_{p.Player1LastName}_{p.Player1Age}".Replace(" ", "_"),
                    DisplayName = $"{p.Player1FirstName} {p.Player1LastName} ({DateTime.Now.Year - p.Player1Age})"
                });
            }

            foreach (var p in player2s)
            {
                // Lägg bara till om spelaren inte redan finns (baserat på en unik kombination)
                if (!allPlayers.Any(ap => ap.FirstName == p.Player2FirstName && ap.LastName == p.Player2LastName && ap.Age == p.Player2Age))
                {
                    allPlayers.Add(new PlayerInfoDTO
                    {
                        FirstName = p.Player2FirstName,
                        LastName = p.Player2LastName,
                        Age = p.Player2Age,
                        Id = $"{p.Player2FirstName}_{p.Player2LastName}_{p.Player2Age}".Replace(" ", "_"),
                        DisplayName = $"{p.Player2FirstName} {p.Player2LastName} ({DateTime.Now.Year - p.Player2Age})"
                    });
                }
            }
            return allPlayers
                .GroupBy(p => p.Id)
                .Select(g => g.First())
                .OrderBy(p => p.DisplayName)
                .ToList();
        }

        public bool CheckIfPlayer1HasMatchPoint(int matchId)
        {
            var match = _dbContext.Match.FirstOrDefault(m => m.Id == matchId);
            if (match == null)
                return false;

            // Hur många set krävs för att vinna matchen?
            int setsToWin = (int)Math.Ceiling(match.BestOfSets / 2.0);

            // Hur många set har spelare 1 redan vunnit?
            int setsWonByPlayer1 = _dbContext.Sets
                .Count(s => s.MatchId == matchId && s.WinnerPlayer == match.Player1FirstName);

            // Om spelare 1 är ett set från matchvinst...
            if (setsWonByPlayer1 == setsToWin - 1)
            {
                // ...och leder nuvarande set och kan vinna med nästa poäng:
                var currentSet = _dbContext.Sets.FirstOrDefault(
                    s => s.MatchId == matchId && s.WinnerPlayer == null
                );

                if (currentSet == null)
                    return false;

                int nextScore = currentSet.Player1Score + 1;
                int lead = nextScore - currentSet.Player2Score;

                if (nextScore >= 11 && lead >= 2)
                {
                    return true; // MATCH POINT!
                }
            }

            return false;
        }


        public bool CheckIfPlayer2HasMatchPoint(int matchId)
        {
            var match = _dbContext.Match.FirstOrDefault(m => m.Id == matchId);
            if (match == null)
                return false;

            int setsToWin = (int)Math.Ceiling(match.BestOfSets / 2.0);
            int setsWonByPlayer2 = _dbContext.Sets
                .Count(s => s.MatchId == matchId && s.WinnerPlayer == match.Player2FirstName);

            if (setsWonByPlayer2 == setsToWin - 1)
            {
                var currentSet = _dbContext.Sets.FirstOrDefault(
                    s => s.MatchId == matchId && s.WinnerPlayer == null
                );

                if (currentSet == null)
                    return false;

                int nextScore = currentSet.Player2Score + 1;
                int lead = nextScore - currentSet.Player1Score;

                if (nextScore >= 11 && lead >= 2)
                {
                    return true;
                }
            }

            return false;
        }

        public int Player1WonSets(int matchId)
        {
            var match = _dbContext.Match.FirstOrDefault(m => m.Id == matchId);
            match.Player1WonSets++;
            _dbContext.Update(match);
            _dbContext.SaveChanges();
            return match.Player1WonSets;
        }

        public int Player2WonSets(int matchId)
        {
            var match = _dbContext.Match.FirstOrDefault(m => m.Id == matchId);
            match.Player2WonSets++;
            _dbContext.Update(match);
            _dbContext.SaveChanges();
            return match.Player2WonSets;
        }



        //// TO DO
        //public SetsDTO AddPointToPlayer2(int matchId)
        //{
        //    var match = _dbContext.Sets.FirstOrDefault(m => m.Id == matchId);
        //    var matchDTO = new SetsDTO
        //    {
        //        Player2Score = match.Player2Score,
        //    };
        //    match.Player2Score += matchDTO.Player2Score;
        //    _dbContext.Update(match);
        //    _dbContext.SaveChanges();
        //    CheckEndOfSet(matchId);
        //    return matchDTO;
        //}

        //public string CheckEndOfSet(int matchId)
        //{
        //    var match = _dbContext.Sets.FirstOrDefault(m => m.Id == matchId);
        //    if (match.Player1Score >= 11 || match.Player2Score >= 11)
        //    {
        //        if (Math.Abs(match.Player1Score - match.Player2Score) >= 2)
        //        {
        //            // Kontrollera vem som har flest poäng för att avgöra vinnaren
        //            if (match.Player1Score > match.Player2Score)
        //            {
        //                return "Player1";
        //            }
        //            else
        //            {
        //                return "Player2";
        //            }
        //        }
        //    }
        //    return null;
        //}

        //public void UpdateServe(int matchId)
        //{
        //    var match = _dbContext.Sets.FirstOrDefault(m => m.Id == matchId);

        //    // Öka ServeCounter varje gång en poäng läggs till
        //    match.ServeCounter++;

        //    // När ServeCounter når 2, växla servern och nollställ räknaren
        //    if (match.ServeCounter >= 2)
        //    {
        //        match.IsPlayer1Serve = !match.IsPlayer1Serve; // Växla servern
        //        match.ServeCounter = 0; // Nollställ räknaren
        //    }
        //    _dbContext.SaveChanges();
        //}

    }
}
