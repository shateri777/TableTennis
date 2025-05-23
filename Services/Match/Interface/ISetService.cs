using DataAccessLayer.Data.DTO;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Match.Interface
{
    public interface ISetService
    {
        void CreateSet(int matchId);
        int AddPointToPlayer1(int matchId);
        int AddPointToPlayer2(int matchId);
        int GetPlayer1Score(int matchId);
        int GetPlayer2Score(int matchId);
        bool UpdateServe(int matchId);
        string CheckEndOfSet(int matchId);
        void SetWinnerPlayer(int matchId, string winner);
        SetsDTO GetActiveSetId(int matchId);
        int GetSetsWonByPlayerName(int matchId, string playerName);
        int RemovePointPlayer1(int matchId);
        int RemovePointPlayer2(int matchId);
    }
}
