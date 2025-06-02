namespace TableTennis.ViewModels
{
    public class StatisticsVM
    {
        public string PlayerFullName { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int TotalGamesPlayed { get; set; }
        public string WinPercentage { get; set; }
        public int LongestMatch { get; set; }
        public int FastestMatch { get; set; }
        public string BestAgainstName { get; set; }
        public string WorstAgainstName { get; set; }
        public string BestAgainstWinRate { get; set; }
        public string WorstAgainstWinRate { get; set; }
    }
}
