using DataAccessLayer.Data.DTO;

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
        bool RevertServe(int matchId);
        bool CheckIfPlayer1HasSetPoint(int matchId);
        bool CheckIfPlayer2HasSetPoint(int matchId);
        bool CheckIfDeuce(int matchId);
        void UpdateSet(SetsDTO setDTO);
        SetsDTO GetActiveSetAsDTO(int matchId);
        IEnumerable<SetsInfoDTO> GetSetsForMatch(int matchId);
    }
}
