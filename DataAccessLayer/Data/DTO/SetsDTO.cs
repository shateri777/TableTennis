using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.DTO
{
    public class SetsDTO
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public bool IsPlayer1Serve { get; set; }
        public int ServeCounter { get; set; }
        public string? WinnerPlayer { get; set; }
        public int SetTime { get; set; }
        public bool IsActive { get; set; }

    }
}
