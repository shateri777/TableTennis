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
    }
}
