using DataAccessLayer.Data.DTO;

namespace Services.Player.Interface
{
    public interface IPlayerService
    {
        Dictionary<string, LeaderboardEntryDTO> GetTopPlayers(List<MatchDTO> allMatches);
    }
}
