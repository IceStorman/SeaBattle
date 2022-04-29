using System;

namespace SeaBattle
{
    public class ShootLogic
    {
        private static ConsoleKey shootKey = ConsoleKey.Enter;

        public static (char, bool) Shoot(ConsoleKeyInfo key, char[,] field, int playerX, int playerY, bool isFirstPlayerTurn)
        {
            char cell = field[playerX, playerY];

            if (key.Key == shootKey)
            {
                if (field[playerX, playerY] == GameIcons.ship)
                {
                    cell = GameIcons.destroyedShip;
                }
                else if (field[playerX, playerY] == GameIcons.emptyCell)
                {
                    cell = GameIcons.damagedCell;
                    isFirstPlayerTurn = !isFirstPlayerTurn;
                }
            }

            return (cell, isFirstPlayerTurn);
        }

        public static int CountOfShips(ConsoleKeyInfo key, char[,] field, int playerX, int playerY, int numberOfPlayerShips)
        {
            int num = numberOfPlayerShips;
            if (key.Key == shootKey)
            {
                if (field[playerX, playerY] == GameIcons.ship)
                {
                    num--;
                }
            }
            return num;
        }
    }
}
