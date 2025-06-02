namespace TableTennis.ViewModels
{
    public class MatchHistoryVM
    {
        public int Id { get; set; }
        public string Player1FullName { get; set; }
        public string Player2FullName { get; set; }
        public int Player1Age { get; set; }
        public int Player2Age { get; set; }
        public string SetGender { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public DateTime MatchDate { get; set; }
        public int BestOfSets { get; set; }
        public string? WinnerPlayer { get; set; }
        public string DisplayText { get; set; }
        // Nya egenskaper
        public bool IsPlayer1Winner { get; set; }
        public bool IsPlayer2Winner { get; set; }
        public List<string> SetScoresDetails { get; set; } = new List<string>(); // För poängen i varje set
    }
}
