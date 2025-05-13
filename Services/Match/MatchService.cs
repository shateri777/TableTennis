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

        public void CreateMatch(SetsDTO setDTO)
        {
            var set = new DataAccessLayer.Data.Models.TableTennisMatch
            {
                Player1FirstName = setDTO.Player1FirstName,
                Player1LastName = setDTO.Player1LastName,
                Player2FirstName = setDTO.Player2FirstName,
                Player2LastName = setDTO.Player2LastName,
                Player1Age = setDTO.Player1Age,
                Player2Age = setDTO.Player2Age,
                SetGender = setDTO.SetGender,
                MatchDate = setDTO.MatchDate
            };
            _dbContext.Match.Add(set);
            _dbContext.SaveChanges();
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
