using DataAccessLayer.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Player.Interface
{
    public interface IPlayerService
    {
        Dictionary<string, LeaderboardEntryDTO> GetTopPlayers(List<MatchDTO> allMatches);
    }
}
