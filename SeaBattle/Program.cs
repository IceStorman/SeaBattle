using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle
{
    internal class Program
    {
        private static ConsoleKey shootKey = ConsoleKey.Enter;

        private static char damagedCell = '-';
        private static char destroyedShip = '=';
        private static char ship = '+';
        private static char emptyCell = ' ';

        public static int player1X = 1;
        public static int player1Y = 1;
        public static int player2X = 1;
        public static int player2Y = 1;

        private static int numberOfShips = 10;
        private static int numberOfPlayer1Ships = numberOfShips;
        private static int numberOfPlayer2Ships = numberOfShips;

        private static bool isFirstPlayerTurn = true;

        static void Main(string[] args)
        {
            char[,] firstField = InitialArrangement.FieldGenerating(numberOfPlayer1Ships);
            char[,] secondField = InitialArrangement.FieldGenerating(numberOfPlayer2Ships);

            while (!IsGameEnd())
            {
                InitialArrangement.FieldDrawing(firstField, player1X, player1Y);
                InitialArrangement.FieldDrawing(secondField, player2X, player2Y);

                Console.WriteLine(numberOfPlayer1Ships);
                Console.WriteLine(numberOfPlayer2Ships);

                ConsoleKeyInfo key = Console.ReadKey();

                (int dx, int dy) = MovementAcrossTheField.MovingInput(key);

                if (isFirstPlayerTurn)
                {
                    (int newX, int newY) = MovementAcrossTheField.MoveLogic(dx, dy, player1X, player1Y);

                    if (MovementAcrossTheField.CanMove(firstField, newX, newY))
                    {
                        (player1X, player1Y) = MovementAcrossTheField.Move(newX, newY);
                    }
                    firstField[player1X, player1Y] = Shoot(key, firstField, player1X, player1Y);
                }
                else
                {
                    (int newX, int newY) = MovementAcrossTheField.MoveLogic(dx, dy, player2X, player2Y);

                    if(MovementAcrossTheField.CanMove(secondField, newX, newY))
                    {
                        (player2X, player2Y) = MovementAcrossTheField.Move(newX, newY);
                    }
                    secondField[player2X, player2Y] = Shoot(key, secondField, player2X, player2Y);
                }

                Console.Clear();
            }
            Console.WriteLine("Game ended");

            Console.ReadKey();
        }

        private static char Shoot(ConsoleKeyInfo key, char[,] field, int playerX, int playerY)
        {
            char cell = field[playerX, playerY];

            if (key.Key == shootKey)
            {
                if (field[playerX, playerY] == ship)
                {
                    cell = destroyedShip;
                }
                else if(field[playerX, playerY] == emptyCell)
                {
                    cell = damagedCell;
                    isFirstPlayerTurn = !isFirstPlayerTurn;
                }
            }

            return cell;
        }

        private static void CountOfShips(ConsoleKeyInfo key, char[,] field, int playerX, int playerY, int numberOfPlayerShips)
        {
            int num = numberOfPlayerShips;
            if(key.Key == shootKey)
            {
                if (field[playerX, playerY] == ship)
                {
                    num--;
                }
            }
        }

        private static bool IsGameEnd()
        {
            if (numberOfPlayer1Ships <= 0 || numberOfPlayer2Ships <= 0) return true;
            return false;
        }
    }
}
