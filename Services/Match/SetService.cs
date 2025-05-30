using DataAccessLayer.Data.DTO;
using DataAccessLayer.Data;
using Services.Match.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Data.Models;
using System.Text.RegularExpressions;
using Microsoft.Extensions.FileSystemGlobbing;

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
                WinnerPlayer = null,
            };
            _dbContext.Sets.Add(set);
            _dbContext.SaveChanges();
        }

        public int AddPointToPlayer1(int matchId)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.MatchId == matchId && m.WinnerPlayer == null);
            if (set != null)
            {
                set.Player1Score++;
                _dbContext.Update(set);
                _dbContext.SaveChanges();
                int score = set.Player1Score;

                return score;
            }
            return 0;
        }

        public int AddPointToPlayer2(int matchId)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.MatchId == matchId && m.WinnerPlayer == null);
            if (set != null)
            {
                set.Player2Score++;
                _dbContext.Update(set);
                _dbContext.SaveChanges();
                int score = set.Player2Score;

                return score;
            }
            return 0;
        }

        public int GetPlayer1Score(int matchId)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.MatchId == matchId && m.WinnerPlayer == null);
            if (set != null)
            {
                return set.Player1Score;
            }
            return 0;
        }

        public int GetPlayer2Score(int matchId)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.MatchId == matchId && m.WinnerPlayer == null);
            if (set != null)
            {
                return set.Player2Score;
            }
            return 0;
        }


        public bool UpdateServe(int matchId)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.MatchId == matchId && m.WinnerPlayer == null);

            // Öka ServeCounter varje gång en poäng läggs till
            set.ServeCounter++;

            if (set.Player1Score >= 10 && set.Player2Score >= 10)
            {
                // Om båda spelare har minst 10 poäng, växla servern efter varje poäng
                set.IsPlayer1Serve = !set.IsPlayer1Serve; // Växla servern
                set.ServeCounter = 0; // Nollställ räknaren
            }
            else
            {

                // När ServeCounter når 2, växla servern och nollställ räknaren
                if (set.ServeCounter >= 2)
                {
                    set.IsPlayer1Serve = !set.IsPlayer1Serve; // Växla servern
                    set.ServeCounter = 0; // Nollställ räknaren
                }
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
            var match = _dbContext.Sets.FirstOrDefault(m => m.MatchId == matchId && m.WinnerPlayer == null);
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

        public void UpdateSet(SetsDTO setDTO)
        {
            var setEntity = _dbContext.Sets.Find(setDTO.Id);
            if (setEntity != null)
            {
                setEntity.Player1Score = setDTO.Player1Score;
                setEntity.Player2Score = setDTO.Player2Score;
                setEntity.WinnerPlayer = setDTO.WinnerPlayer;
                setEntity.SetTime = setDTO.SetTime;
                setEntity.IsPlayer1Serve = setDTO.IsPlayer1Serve;
                setEntity.ServeCounter = setDTO.ServeCounter;
                _dbContext.SaveChanges();
            }
        }

        public bool CheckIfPlayer1HasSetPoint(int matchId)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.MatchId == matchId && m.WinnerPlayer == null);

            if (set == null)
                return false;

            int nextScore = set.Player1Score + 1;
            int newLead = nextScore - set.Player2Score;

            // Nästa poäng skulle ge minst 11 och minst 2 poängs ledning
            if (nextScore >= 11 && newLead >= 2)
            {
                return true;
            }

            return false;
        }





        //public bool CheckIfPlayer1HasSetPoint(int matchId)
        //{
        //    var set = _dbContext.Sets.FirstOrDefault(m => m.MatchId == matchId && m.WinnerPlayer == null);

        //    //bool player1HasSetPoint = set.Player1Score >= 10 && (set.Player1Score - set.Player2Score) == 1;

        //    if (set.Player1Score >= 10 || set.Player2Score >= 10)
        //    {
        //        if (Math.Abs(set.Player1Score - set.Player2Score) >= 1)
        //        {
        //            // Kontrollera vem som har flest poäng för att avgöra vinnaren
        //            if (set.Player1Score > set.Player2Score)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }


        //    return false;
        //}

        public bool CheckIfPlayer2HasSetPoint(int matchId)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.MatchId == matchId && m.WinnerPlayer == null);

            //bool player2HasSetPoint = set.Player2Score >= 10 && (set.Player2Score - set.Player1Score) == 1;

            if (set.Player1Score >= 10 || set.Player2Score >= 10)
            {
                if (Math.Abs(set.Player1Score - set.Player2Score) >= 1)
                {
                    // Kontrollera vem som har flest poäng för att avgöra vinnaren
                    if (set.Player2Score > set.Player1Score)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }


            return false;
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
            var setId = _dbContext.Sets.FirstOrDefault(set => set.MatchId == matchId && set.WinnerPlayer == null);
            var setDTO = new SetsDTO
            {
                Player1Score = 0,
                WinnerPlayer = null,
                Player2Score = 0,
                ServeCounter = 0,
                IsPlayer1Serve = true,
            };
            return setDTO;
        }
        public SetsDTO GetActiveSetAsDTO(int matchId)
        {
            var activeSetEntity = _dbContext.Sets.FirstOrDefault(set => set.MatchId == matchId && set.WinnerPlayer == null);

            if (activeSetEntity != null)
            {
                return new SetsDTO
                {
                    Id = activeSetEntity.Id,
                    Player1Score = activeSetEntity.Player1Score,
                    Player2Score = activeSetEntity.Player2Score,
                    WinnerPlayer = activeSetEntity.WinnerPlayer,
                    ServeCounter = activeSetEntity.ServeCounter,
                    IsPlayer1Serve = activeSetEntity.IsPlayer1Serve,
                    SetTime = activeSetEntity.SetTime, 
                    IsActive = activeSetEntity.IsActive,
                    MatchId = activeSetEntity.MatchId  
                };
            }
            return null;
        }
        public int GetSetsWonByPlayerName(int matchId, string playerName)
        {
            return _dbContext.Sets.Count(s => s.MatchId == matchId && s.WinnerPlayer == playerName);
        }


        public int RemovePointPlayer1(int matchId)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.MatchId == matchId && m.WinnerPlayer == null);
            if (set.Player1Score == 0)
            {
                return 0;
            }
            if (set != null)
            {
                set.Player1Score--;
                _dbContext.Update(set);
                _dbContext.SaveChanges();
                int score = set.Player1Score;

                return score;
            }
            return 0;
        }

        public int RemovePointPlayer2(int matchId)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.MatchId == matchId && m.WinnerPlayer == null);
            if (set.Player2Score == 0)
            {
                return 0;
            }

            if (set != null)
            {
                set.Player2Score--;
                _dbContext.Update(set);
                _dbContext.SaveChanges();
                int score = set.Player2Score;

                return score;
            }
            return 0;
        }

        public bool RevertServe(int matchId)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.MatchId == matchId && m.WinnerPlayer == null);


            // Om ServeCounter är 0 → vi behöver växla tillbaka och sätta counter till 1
            if (set.ServeCounter == 0)
            {
                set.IsPlayer1Serve = !set.IsPlayer1Serve;
                set.ServeCounter = 1;
            }
            else
            {
                set.ServeCounter--; // Annars backar vi bara räknaren
            }

            _dbContext.SaveChanges();

            return set.IsPlayer1Serve;
        }

        public bool CheckIfDeuce(int matchId)
        {
            var set = _dbContext.Sets.FirstOrDefault(m => m.MatchId == matchId && m.WinnerPlayer == null);

            if (set.Player1Score >= 10 && set.Player2Score >= 10)
            {
                return true; // Deuce
            }

            return false; // Not deuce
        }

        
    }
}
