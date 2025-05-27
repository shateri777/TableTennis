using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.DTO
{
    public class MatchDTO
    {
        public int Id { get; set; }
        public string Player1FirstName { get; set; }
        public string Player1LastName { get; set; }
        public string Player2FirstName { get; set; }
        public string Player2LastName { get; set; }
        public int Player1Age { get; set; }
        public int Player2Age { get; set; }
        public string SetGender { get; set; }
        public DateTime MatchDate { get; set; }
        public int BestOfSets { get; set; }
        public string? WinnerPlayer { get; set; }
        public int TotalMatchTime { get; set; }
        public int Player1WonSets { get; set; }
        public int Player2WonSets { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
