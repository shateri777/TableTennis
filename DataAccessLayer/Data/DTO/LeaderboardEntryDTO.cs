using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.DTO
{
    public class LeaderboardEntryDTO
    {
        public string PlayerFullName { get; set; }
        public int Wins { get; set; }
        public int TotalGamesPlayed { get; set; }
        public double WinPercentage => TotalGamesPlayed > 0 ? (double)Wins / TotalGamesPlayed : 0;
    }
}
