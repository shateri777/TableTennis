namespace TableTennis.ViewModels
{
    public class LeaderboardEntryVM
    {
        public int Rank { get; set; }
        public string PlayerFullName { get; set; }
        public int Wins { get; set; }
        public int TotalGamesPlayed { get; set; }
        public double WinPercentage { get; set; }
    }
}
