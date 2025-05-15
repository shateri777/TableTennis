using DataAccessLayer.Data.DTO;
using DataAccessLayer.Data;
using Services.Match.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Data.Models;

namespace Services.Match
{
    public class SetService : ISetService
    {
        private readonly ApplicationDbContext _dbContext;

        public SetService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateSet(int matchId)
        {
            var set = new TableTennisSet
            {
                MatchId = matchId,
            };
            _dbContext.Sets.Add(set);
            _dbContext.SaveChanges();

        }

        public int AddPointToPlayer1(int matchId)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.Id == matchId);
            if (set != null)
            {
                set.Player1Score++;
                _dbContext.Update(set);
                _dbContext.SaveChanges();
                int score = set.Player1Score;

                return score;
                //CheckEndOfSet(matchId);
            }
            return 0;
        }

        public int AddPointToPlayer2(int matchId)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.Id == matchId);
            if (set != null)
            {
                set.Player2Score++;
                _dbContext.Update(set);
                _dbContext.SaveChanges();
                int score = set.Player2Score;

                return score;
                //CheckEndOfSet(matchId);
            }
            return 0;
        }

        public int GetPlayer1Score(int matchId)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.Id == matchId);
            if (set != null)
            {
                return set.Player1Score;
            }
            return 0;
        }

        public int GetPlayer2Score(int matchId)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.Id == matchId);
            if (set != null)
            {
                return set.Player2Score;
            }
            return 0;
        }

        public bool UpdateServe(int matchId)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.Id == matchId);

            // Öka ServeCounter varje gång en poäng läggs till
            set.ServeCounter++;

            // När ServeCounter når 2, växla servern och nollställ räknaren
            if (set.ServeCounter >= 2)
            {
                set.IsPlayer1Serve = !set.IsPlayer1Serve; // Växla servern
                set.ServeCounter = 0; // Nollställ räknaren
            }
            _dbContext.SaveChanges();

            if (set.IsPlayer1Serve)
            {
                return true; // Player 1 serves
            }
            else
            {
                return false; // Player 2 serves
            }
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

        public void SetWinnerPlayer(int matchId, string winner)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.Id == matchId);
            if (set != null)
            {
                set.WinnerPlayer = winner;
                _dbContext.Update(set);
                _dbContext.SaveChanges();
            }
        }
    }
}
