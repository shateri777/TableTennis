namespace Pingis.ViewModels
{
    public class TableTennisMatchVM
    {
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public bool IsPlayer1Serve { get; set; } = true;
        public int ServeCounter { get; set; } = 0;

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

        public void CheckEndOfSet()
        {
            if ((Player1Score >= 11 || Player2Score >= 11) && Math.Abs(Player1Score - Player2Score) >= 2)
            {
                // Markera set som över och hantera logik
            }
            // UpdateServe();
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
