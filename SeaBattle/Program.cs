using System;

namespace SeaBattle
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Player player1 = new Player();
            Player player2 = new Player();

            SetStartParameters(player1);
            SetStartParameters(player2);

            InitialArrangement.FieldGenerating(player1);
            InitialArrangement.FieldGenerating(player2);
            
            while (!EndGameLogic.IsGameEnd(player1.numberOfShips, player2.numberOfShips))
            {
                InitialArrangement.FieldDrawing(player1);
                InitialArrangement.FieldDrawing(player2);

                Console.WriteLine("Player1 ships: " + player1.numberOfShips);
                Console.WriteLine("Player2 ships: " + player2.numberOfShips);

                ConsoleKeyInfo key = Console.ReadKey();

                (int dx, int dy) = MovementAcrossTheField.MovingInput(key);

                if (player1.isPlayerTurn)
                {
                    Move(player1, player2, key, dx, dy);
                }
                else if(player2.isPlayerTurn)
                {
                    Move(player2, player1, key, dx, dy);
                }

                Console.Clear();
            }
            EndGameLogic.EndGameMessage(player1.numberOfShips, player2.numberOfShips);

            Console.ReadKey();
        }

        private static void SetStartParameters(Player player)
        {
            player.SetXPos(1);
            player.SetYPos(1);
        }

        private static void Move(Player currentPlayer, Player otherPlayer, ConsoleKeyInfo key, int dx, int dy)
        {
            ShootLogic.CountOfShips(key, currentPlayer);

            (int newX, int newY) = MovementAcrossTheField.MoveLogic(dx, dy, currentPlayer);

            if (MovementAcrossTheField.CanMove(currentPlayer.field, newX, newY))
            {
                MovementAcrossTheField.Move(currentPlayer, newX, newY);
            }

            currentPlayer.field[currentPlayer.xPos, currentPlayer.yPos] = ShootLogic.Shoot(key, currentPlayer, otherPlayer);
        }
    }
}