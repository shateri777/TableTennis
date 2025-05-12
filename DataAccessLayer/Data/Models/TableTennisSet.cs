namespace TableTennis.Data.Models
{
    public class TableTennisSet
    {
        public int Id { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public bool IsPlayer1Serve { get; set; } = true;
        public int ServeCounter { get; set; } = 0;

        // Player information
        public string Player1UserName { get; set; }
        public string Player2UserName { get; set; }
        public int Player1Age { get; set; }
        public int Player2Age { get; set; }
        public string SetGender { get; set; }
        public DateOnly MatchDate { get; set; }
        public string WinnerPlayer { get; set; }

        public void AddPointToPlayer1()
        {
            Player1Score++;
            CheckEndOfSet();
        }

        public void AddPointToPlayer2()
        {
            Player2Score++;
            CheckEndOfSet();
        }

        public string CheckEndOfSet()
        {
            if (Player1Score >= 11 || Player2Score >= 11)
            {
                if (Math.Abs(Player1Score - Player2Score) >= 2)
                {
                    // Kontrollera vem som har flest poäng för att avgöra vinnaren
                    if (Player1Score > Player2Score)
                    {
                        return "Player1";
                    }
                    else
                    {
                        return "Player2";
                    }
                }
            }
            return null;
        }

        public void UpdateServe()
        {
            // Öka ServeCounter varje gång en poäng läggs till
            ServeCounter++;

            // När ServeCounter når 2, växla servern och nollställ räknaren
            if (ServeCounter >= 2)
            {
                IsPlayer1Serve = !IsPlayer1Serve; // Växla servern
                ServeCounter = 0; // Nollställ räknaren
            }
        }
    }
}
