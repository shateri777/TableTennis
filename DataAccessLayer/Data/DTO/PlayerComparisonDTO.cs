using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.DTO
{
    public class PlayerComparisonDTO
    {
        public string Player1Name { get; set; }
        public string Player2Name { get; set; }
        public int TotalMatches { get; set; }
        public int Player1Wins { get; set; }
        public int Player2Wins { get; set; }
        public string Player1WinRate { get; set; }
        public string Player2WinRate { get; set; }

    }

}
