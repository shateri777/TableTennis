namespace TableTennis.ViewModels
{
    public class PlayerComparisonVM
    {
        public string Player1Name { get; set; } = string.Empty;
        public string Player2Name { get; set; } = string.Empty;
        public int TotalMatches { get; set; }
        public int Player1Wins { get; set; }
        public int Player2Wins { get; set; }
        public string Player1WinRate { get; set; } = "0%";
        public string Player2WinRate { get; set; } = "0%";
    }


}
