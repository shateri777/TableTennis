using DataAccessLayer.Data.DTO;
using DataAccessLayer.Data;
using Services.Match.Interface;
using System;
using System.Linq;
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
                Player1Score = 0,
                Player2Score = 0,
                IsPlayer1Serve = true,
                ServeCounter = 0,
                WinnerPlayer = null
            };
            _dbContext.Sets.Add(set);
            _dbContext.SaveChanges();
        }

        // vinstvillkor
        private bool IsSetWon(int score, int otherScore)
        {
            return score >= 11 && (score - otherScore) >= 2;
        }

        public int AddPointToPlayer1(int matchId)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.MatchId == matchId && m.WinnerPlayer == null);
            if (set == null) return 0;

            if (IsSetWon(set.Player1Score, set.Player2Score) || IsSetWon(set.Player2Score, set.Player1Score))
                return set.Player1Score; // Set är redan vunnet

            set.Player1Score++;
            UpdateServeAndCheckWin(set);

            _dbContext.Update(set);
            _dbContext.SaveChanges();

            return set.Player1Score;
        }

        public int AddPointToPlayer2(int matchId)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.MatchId == matchId && m.WinnerPlayer == null);
            if (set == null) return 0;

            if (IsSetWon(set.Player1Score, set.Player2Score) || IsSetWon(set.Player2Score, set.Player1Score))
                return set.Player2Score;

            set.Player2Score++;
            UpdateServeAndCheckWin(set);

            _dbContext.Update(set);
            _dbContext.SaveChanges();

            return set.Player2Score;
        }

        public int ChangePlayer1Score(int matchId, int delta)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.MatchId == matchId && m.WinnerPlayer == null);
            if (set == null) return 0;

            if (IsSetWon(set.Player1Score, set.Player2Score) || IsSetWon(set.Player2Score, set.Player1Score))
                return set.Player1Score;

            set.Player1Score = Math.Max(0, set.Player1Score + delta);
            UpdateServeAndCheckWin(set);

            _dbContext.Update(set);
            _dbContext.SaveChanges();
            return set.Player1Score;
        }

        public int ChangePlayer2Score(int matchId, int delta)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.MatchId == matchId && m.WinnerPlayer == null);
            if (set == null) return 0;

            if (IsSetWon(set.Player1Score, set.Player2Score) || IsSetWon(set.Player2Score, set.Player1Score))
                return set.Player2Score;

            set.Player2Score = Math.Max(0, set.Player2Score + delta);
            UpdateServeAndCheckWin(set);

            _dbContext.Update(set);
            _dbContext.SaveChanges();
            return set.Player2Score;
        }

        // kontrollera och sätt vinnare
        private void UpdateServeAndCheckWin(TableTennisSet set)
        {
            set.ServeCounter++;
            if (set.ServeCounter >= 2)
            {
                set.IsPlayer1Serve = !set.IsPlayer1Serve;
                set.ServeCounter = 0;
            }

            if (IsSetWon(set.Player1Score, set.Player2Score) && set.WinnerPlayer == null)
                set.WinnerPlayer = "Player1";
            else if (IsSetWon(set.Player2Score, set.Player1Score) && set.WinnerPlayer == null)
                set.WinnerPlayer = "Player2";
        }

        public int GetPlayer1Score(int matchId)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.MatchId == matchId && m.WinnerPlayer == null);
            return set?.Player1Score ?? 0;
        }

        public int GetPlayer2Score(int matchId)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.MatchId == matchId && m.WinnerPlayer == null);
            return set?.Player2Score ?? 0;
        }

        public bool UpdateServe(int matchId)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.MatchId == matchId && m.WinnerPlayer == null);
            if (set == null) return true;

            set.ServeCounter++;
            if (set.ServeCounter >= 2)
            {
                set.IsPlayer1Serve = !set.IsPlayer1Serve;
                set.ServeCounter = 0;
            }
            _dbContext.SaveChanges();

            return set.IsPlayer1Serve;
        }

        public string CheckEndOfSet(int matchId)
        {
            var match = _dbContext.Sets.FirstOrDefault(m => m.MatchId == matchId && m.WinnerPlayer == null);
            if (match == null) return null;
            if (IsSetWon(match.Player1Score, match.Player2Score))
                return "Player1";
            if (IsSetWon(match.Player2Score, match.Player1Score))
                return "Player2";
            return null;
        }

        public void SetWinnerPlayer(int matchId, string winner)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.MatchId == matchId && m.WinnerPlayer == null);
            if (set != null)
            {
                set.WinnerPlayer = winner;
                _dbContext.Update(set);
                _dbContext.SaveChanges();
            }
        }

        public SetsDTO GetActiveSetId(int matchId)
        {
            var set = _dbContext.Sets.FirstOrDefault(set => set.MatchId == matchId && set.WinnerPlayer == null);
            if (set != null)
            {
                return new SetsDTO
                {
                    Player1Score = set.Player1Score,
                    WinnerPlayer = set.WinnerPlayer,
                    Player2Score = set.Player2Score,
                    ServeCounter = set.ServeCounter,
                    IsPlayer1Serve = set.IsPlayer1Serve
                };
            }
            return null;
        }

        public int GetSetsWonByPlayerName(int matchId, string playerName)
        {
            return _dbContext.Sets.Count(s => s.MatchId == matchId && s.WinnerPlayer == playerName);
        }

        public int GetSetCount(int matchId)
        {
            return _dbContext.Sets.Count(s => s.MatchId == matchId);
        }
    }
}
