using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.DTO
{
    public class SetsInfoDTO
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public string? WinnerPlayer { get; set; }
        public bool IsActive { get; set; }
    }
}
