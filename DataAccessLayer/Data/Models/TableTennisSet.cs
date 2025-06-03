using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Data.Models
{
    public class TableTennisSet
    {
        public int Id { get; set; }
        public int MatchId { get; set; } 
        public TableTennisMatch Match { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public bool IsPlayer1Serve { get; set; }
        public int ServeCounter { get; set; }
        public string? WinnerPlayer { get; set; }
        public bool IsActive { get; set; }
        public int SetTime { get; set; }

    }
}
