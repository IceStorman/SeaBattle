using System;

namespace SeaBattle
{
    internal class Program
    {
        private static Player player1;
        private static Player player2;

        private static bool isFirstPlayerTurn = true;

        private static void Main(string[] args)
        {
            player1 = SetStartParameters(player1);
            player2 = SetStartParameters(player2);

            player1.field = InitialArrangement.FieldGenerating(player1.numberOfShips);
            player2.field = InitialArrangement.FieldGenerating(player2.numberOfShips);
            
            while (!EndGameLogic.IsGameEnd(player1.numberOfShips, player2.numberOfShips))
            {
                InitialArrangement.FieldDrawing(player1.field, player1.xPos, player1.yPos);
                InitialArrangement.FieldDrawing(player2.field, player2.xPos, player2.yPos);

                Console.WriteLine("Player1 ships: " + player1.numberOfShips);
                Console.WriteLine("Player2 ships: " + player2.numberOfShips);

                ConsoleKeyInfo key = Console.ReadKey();

                (int dx, int dy) = MovementAcrossTheField.MovingInput(key);

                if (isFirstPlayerTurn)
                {
                    player1 = Move(player1, key, dx, dy);
                }
                else
                {
                    player2 = Move(player2, key, dx, dy);
                }             

                Console.Clear();
            }
            EndGameLogic.EndGameMessage(player1.numberOfShips, player2.numberOfShips);

            Console.ReadKey();
        }

        private static Player SetStartParameters(Player player)
        {
            player.xPos = 1;
            player.yPos = 1;
            player.numberOfShips = 10;

            return player;
        }

        private static Player Move(Player player, ConsoleKeyInfo key, int dx, int dy)
        {
            player.numberOfShips = ShootLogic.CountOfShips(key, player.field, player.xPos, player.yPos, player.numberOfShips);

            (int newX, int newY) = MovementAcrossTheField.MoveLogic(dx, dy, player.xPos, player.yPos);

            if (MovementAcrossTheField.CanMove(player.field, newX, newY))
            {
                (player.xPos, player.yPos) = MovementAcrossTheField.Move(newX, newY);
            }

            (player.field[player.xPos, player.yPos], isFirstPlayerTurn) = ShootLogic.Shoot(key, player.field, player.xPos, player.yPos, isFirstPlayerTurn);
            
            return player;
        }
    }
}
