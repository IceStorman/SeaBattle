using System;

namespace SeaBattle
{
    public class EndGameLogic
    {
        public static bool IsGameEnd(int numberOfPlayer1Ships, int numberOfPlayer2Ships)
        {
            if (numberOfPlayer1Ships <= 0 || numberOfPlayer2Ships <= 0) return true;
            return false;
        }

        public static void EndGameMessage(int numberOfPlayer1Ships, int numberOfPlayer2Ships)
        {
            if (numberOfPlayer1Ships <= 0)
            {
                Console.WriteLine("Player2 won!");
            }
            else if (numberOfPlayer2Ships <= 0)
            {
                Console.WriteLine("Player1 won!");
            }
        }
    }
}
