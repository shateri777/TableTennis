using DataAccessLayer.Data.DTO;
using Services.Match.Interface;
using DataAccessLayer.Data;

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
                    MatchDate = match.MatchDate
                };
            }
            return null;
        }
        public string CheckMatchWinner(int matchId) 
        {
            var match = _dbContext.Match.FirstOrDefault(m => m.Id == matchId);
            var sets = _dbContext.Sets.Where(m => m.MatchId == matchId);
            if (sets != null)
            {
                var player1Wins = sets.Count(s => s.WinnerPlayer == match.Player1FirstName);
                var player2Wins = sets.Count(s => s.WinnerPlayer == match.Player2FirstName);
                int winsNeeded = (match.BestOfSets / 2) + 1;

                if (player1Wins >= winsNeeded)
                {
                    match.WinnerPlayer = match.Player1FirstName;
                    _dbContext.Update(match);
                    _dbContext.SaveChanges();
                    return match.Player1FirstName;
                }
                else if (player2Wins >= winsNeeded)
                {
                    match.WinnerPlayer = match.Player2FirstName;
                    _dbContext.Update(match);
                    _dbContext.SaveChanges();
                    return match.Player2FirstName;
                }
                else
                {
                    return null;
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
