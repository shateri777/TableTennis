namespace Pingis.ViewModels
{
    public class SetVM
    {
        public int MatchId { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public bool IsPlayer1Serve { get; set; } = true;
        public int ServeCounter { get; set; } = 0;
        public string? WinnerPlayer { get; set; }
        public int TotalMatchTime { get; set; }
        public int SetTime { get; set; }


    }
}
