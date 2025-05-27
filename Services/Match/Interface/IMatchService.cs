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
        int CreateMatch(MatchDTO matchDTO);
        MatchDTO FindMatchId(int matchId);
        string CheckMatchWinner(int matchId);
        List<MatchDTO> GetAllMatches(string searchTerm);
        List<MatchDTO> GetAllInactiveMatches();
        List<PlayerInfoDTO> GetDistinctPlayers();
        bool CheckIfPlayer1HasMatchPoint(int matchId);
        bool CheckIfPlayer2HasMatchPoint(int matchId);
        int Player1WonSets(int matchId);
        int Player2WonSets(int matchId);
        void SoftDeleteMatch(MatchDTO match);
        void RestoreDeletedMatch(int selectedId);
    }
}
