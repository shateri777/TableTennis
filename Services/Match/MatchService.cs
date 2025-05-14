using DataAccessLayer.Data.DTO;
using Services.Match.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void CreateMatch(MatchDTO matchDTO)
        {
            var set = new DataAccessLayer.Data.Models.TableTennisMatch
            {
                Player1FirstName = matchDTO.Player1FirstName,
                Player1LastName = matchDTO.Player1LastName,
                Player2FirstName = matchDTO.Player2FirstName,
                Player2LastName = matchDTO.Player2LastName,
                Player1Age = matchDTO.Player1Age,
                Player2Age = matchDTO.Player2Age,
                SetGender = matchDTO.SetGender,
                MatchDate = matchDTO.MatchDate
            };
            _dbContext.Match.Add(set);
            _dbContext.SaveChanges();
        }


        public MatchDTO findMatchId(int matchId)
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
                    SetGender = match.SetGender,
                    MatchDate = match.MatchDate
                };
            }
            return null;
        }



        // TO DO
        //public SetsDTO AddPointToPlayer1(int matchId)
        //{
        //    var match = _dbContext.Sets.FirstOrDefault(m => m.Id == matchId);
        //    var matchDTO = new SetsDTO
        //    {
        //        Player1Score = match.Player1Score,
        //    };
        //    match.Player1Score += matchDTO.Player1Score;
        //    _dbContext.Update(match);
        //    _dbContext.SaveChanges();
        //    CheckEndOfSet(matchId);
        //    return matchDTO;
        //}
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
