using DataAccessLayer.Data.DTO;
using Services.Match.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableTennis.Data;

namespace Services.Match
{
    public class MatchService : IMatchService
    {
        private readonly ApplicationDbContext _dbContext;

        public MatchService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // TO DO
        public SetsDTO AddPointToPlayer1(int matchId)
        {
            var match = _dbContext.Sets.FirstOrDefault(m => m.Id == matchId);
            var matchDTO = new SetsDTO
            {
                Player1Score = match.Player1Score,
            };
            match.Player1Score += matchDTO.Player1Score;
            _dbContext.Update(match);
            _dbContext.SaveChanges();
            CheckEndOfSet(matchId);
            return matchDTO;
        }
        // TO DO
        public SetsDTO AddPointToPlayer2(int matchId)
        {
            var match = _dbContext.Sets.FirstOrDefault(m => m.Id == matchId);
            var matchDTO = new SetsDTO
            {
                Player2Score = match.Player2Score,
            };
            match.Player2Score += matchDTO.Player2Score;
            _dbContext.Update(match);
            _dbContext.SaveChanges();
            CheckEndOfSet(matchId);
            return matchDTO;
        }

        public string CheckEndOfSet(int matchId)
        {
            var match = _dbContext.Sets.FirstOrDefault(m => m.Id == matchId);
            if (match.Player1Score >= 11 || match.Player2Score >= 11)
            {
                if (Math.Abs(match.Player1Score - match.Player2Score) >= 2)
                {
                    // Kontrollera vem som har flest poäng för att avgöra vinnaren
                    if (match.Player1Score > match.Player2Score)
                    {
                        return "Player1";
                    }
                    else
                    {
                        return "Player2";
                    }
                }
            }
            return null;
        }

        public void UpdateServe(int matchId)
        {
            var match = _dbContext.Sets.FirstOrDefault(m => m.Id == matchId);

            // Öka ServeCounter varje gång en poäng läggs till
            match.ServeCounter++;

            // När ServeCounter når 2, växla servern och nollställ räknaren
            if (match.ServeCounter >= 2)
            {
                match.IsPlayer1Serve = !match.IsPlayer1Serve; // Växla servern
                match.ServeCounter = 0; // Nollställ räknaren
            }
            _dbContext.SaveChanges();
        }
        public void CreateSet(SetsDTO setDTO)
        {
            var set = new TableTennis.Data.Models.TableTennisSet
            {
                Player1UserName = setDTO.Player1UserName,
                Player2UserName = setDTO.Player2UserName,
                Player1Age = setDTO.Player1Age,
                Player2Age = setDTO.Player2Age,
                SetGender = setDTO.SetGender,           
                Player1Score = setDTO.Player1Score,
                Player2Score = setDTO.Player2Score,
                IsPlayer1Serve = setDTO.IsPlayer1Serve,
                ServeCounter = setDTO.ServeCounter, 
                MatchDate = setDTO.MatchDate,
            };
            _dbContext.Sets.Add(set);
            _dbContext.SaveChanges();
        }
    }
}
