using DataAccessLayer.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Match.Interface
{
    public interface IMatchService
    {
        //SetsDTO AddPointToPlayer1(int matchId);
        //SetsDTO AddPointToPlayer2(int matchId);
        //string CheckEndOfSet(int matchId);
        //void UpdateServe(int matchId);
        int CreateMatch(MatchDTO matchDTO);
        MatchDTO FindMatchId(int matchId);
        string CheckMatchWinner(int matchId);
        List<MatchDTO> GetAllMatches(string searchTerm);
    }
}
