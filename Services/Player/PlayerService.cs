using DataAccessLayer.Data.DTO;
using Services.Player.Interface;

namespace Services.Player
{
    public class PlayerService : IPlayerService
    {
        public Dictionary<string, LeaderboardEntryDTO> GetTopPlayers(List<MatchDTO> allMatches)
        {
            var playerStatsDict = new Dictionary<string, LeaderboardEntryDTO>();
            foreach (var match in allMatches)
            {
                string player1FullName = $"{match.Player1FirstName} {match.Player1LastName}";
                string player2FullName = $"{match.Player2FirstName} {match.Player2LastName}";
                if (!playerStatsDict.ContainsKey(player1FullName))
                {
                    playerStatsDict[player1FullName] = new LeaderboardEntryDTO { PlayerFullName = player1FullName };
                }
                if (!playerStatsDict.ContainsKey(player2FullName))
                {
                    playerStatsDict[player2FullName] = new LeaderboardEntryDTO { PlayerFullName = player2FullName };
                }
                playerStatsDict[player1FullName].TotalGamesPlayed++;
                playerStatsDict[player2FullName].TotalGamesPlayed++;
                if (!string.IsNullOrEmpty(match.WinnerPlayer))
                {
                    string winnerFullName = "";
                    if (match.WinnerPlayer.Equals(match.Player1FirstName, StringComparison.OrdinalIgnoreCase))
                    {
                        winnerFullName = player1FullName;
                    }
                    else if (match.WinnerPlayer.Equals(match.Player2FirstName, StringComparison.OrdinalIgnoreCase))
                    {
                        winnerFullName = player2FullName;
                    }
                    else
                    {
                        winnerFullName = match.WinnerPlayer;
                    }

                    if (playerStatsDict.ContainsKey(winnerFullName))
                    {
                        playerStatsDict[winnerFullName].Wins++;
                    }
                }
            }
            return playerStatsDict;
        }
    }
}
