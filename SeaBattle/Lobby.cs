using System;

namespace SeaBattle
{
    internal class Lobby
    {
        public void StartGame()
        {
            Player player1 = new Player();
            Player player2 = new Player();

            int numberOfRound = 0;

            DefaultNicknames(player1, player2);

            SetPlayerName(player1);
            SetPlayerName(player2);

            int countOfWins = SetCountOfWins();

            while(!IsEndGame(countOfWins, player1, player2))
            {
                numberOfRound++;

                Game game = new Game();
                game.StartRound(player1, player2);
            }
            EndGameLogic(countOfWins, player1, player2);

            Console.ReadKey();
        }

        private void DefaultNicknames(Player player1, Player player2)
        {
            player1.name = "Player1";
            player2.name = "Player2";
        }

        private void SetPlayerName(Player player)
        {
            Console.WriteLine($"{player.name}, set your nickname:");
            player.name = Console.ReadLine();

            Console.Clear();
        }

        private int SetCountOfWins()
        {
            Console.WriteLine("Set how many victories your game will have (more than 0):");
            int countOfWins = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            if(countOfWins > 0)
                return countOfWins;
            return 1;
        }

        private bool IsEndGame(int countOfWins, Player player1, Player player2)
        {
            if (player1.countOfWins >= countOfWins
                || player2.countOfWins >= countOfWins)
                return true;
            return false;
        }

        private void EndGameLogic(int countOfWins, Player player1, Player player2)
        {
            if (player1.countOfWins >= countOfWins)
                Console.WriteLine($"{player1.name} won!");
            else if (player2.countOfWins >= countOfWins)
                Console.WriteLine($"{player2.name} won!");
            Console.WriteLine("Press any key to exit");
        }
    }
}