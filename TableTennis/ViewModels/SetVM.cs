namespace Pingis.ViewModels
{
    public class SetVM
    {
        public string Player1FirstName { get; set; }
        public string Player1LastName { get; set; }
        public string Player2FirstName { get; set; }
        public string Player2LastName { get; set; }
        public int Player1Age { get; set; }
        public int Player2Age { get; set; }
        public string SetGender { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public bool IsPlayer1Serve { get; set; } = true;
        public int ServeCounter { get; set; } = 0;
        public DateTime MatchDate { get; set; }
        public string WinnerPlayer { get; set; }

        //public void AddPointToPlayer1()
        //{
        //    Player1Score++;
        //    CheckEndOfSet();
        //}

        //public void AddPointToPlayer2()
        //{
        //    Player2Score++;
        //    CheckEndOfSet();
        //}

        //public void CheckEndOfSet()
        //{
        //    if ((Player1Score >= 11 || Player2Score >= 11) && Math.Abs(Player1Score - Player2Score) >= 2)
        //    {
        //        // Markera set som över och hantera logik
        //    }
        //    // UpdateServe();
        //}

        //public void UpdateServe()
        //{
        //    // Öka ServeCounter varje gång en poäng läggs till
        //    ServeCounter++;

        //    // När ServeCounter når 2, växla servern och nollställ räknaren
        //    if (ServeCounter >= 2)
        //    {
        //        IsPlayer1Serve = !IsPlayer1Serve; // Växla servern
        //        ServeCounter = 0; // Nollställ räknaren
        //    }
        //}

    }
}
