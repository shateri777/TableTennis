namespace TableTennis.ViewModels
{
    public class MatchHistoryVM
    {
        public string Player1FullName { get; set; }
        public string Player2FullName { get; set; }
        public int Player1Age { get; set; }
        public int Player2Age { get; set; }
        public string SetGender { get; set; }
        public string WinnerPlayer { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public DateTime MatchDate { get; set; }

    }
}
