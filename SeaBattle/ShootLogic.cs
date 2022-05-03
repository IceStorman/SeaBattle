using System;

namespace SeaBattle
{
    public class ShootLogic
    {
        private static ConsoleKey shootKey = ConsoleKey.Enter;

        public static (char, bool) Shoot(ConsoleKeyInfo key, Player player, bool isFirstPlayerTurn)
        {
            char cell = player.field[player.xPos, player.yPos];

            if (key.Key == shootKey)
            {
                if (cell == GameIcons.ship)
                {
                    cell = GameIcons.destroyedShip;
                }
                else if (cell == GameIcons.emptyCell)
                {
                    cell = GameIcons.damagedCell;
                    isFirstPlayerTurn = !isFirstPlayerTurn;
                }
            }

            return (cell, isFirstPlayerTurn);
        }

        public static void CountOfShips(ConsoleKeyInfo key, Player player)
        {
            int num = player.numberOfShips;
            if (key.Key == shootKey)
            {
                if (player.field[player.xPos, player.yPos] == GameIcons.ship)
                {
                    num--;
                }
            }
            player.SetNumberOfShips(num);
        }
    }
}
