using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.DTO
{
    public class Sets
    {
        public int Id { get; set; }
        public string Player1UserName { get; set; }
        public string Player2UserName { get; set; }
        public int Player1Age { get; set; }
        public int Player2Age { get; set; }
        public string SetGender { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public int IsPlayer1Servce { get; set; }
        public int ServeCounter { get; set; }
        public DateOnly MatchDate { get; set; }
        public string WinnerPlayer { get; set; }

    }
}
