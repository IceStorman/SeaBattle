using System;

namespace SeaBattle
{
    public class ShootLogic
    {
        private const ConsoleKey shootKey = ConsoleKey.Enter;

        public static char Shoot(ConsoleKeyInfo key, Player currentPlayer, Player otherPlayer)
        {
            char cell = currentPlayer.field[currentPlayer.xPos, currentPlayer.yPos];

            if (key.Key == shootKey)
            {
                if (cell == GameIcons.ship)
                {
                    cell = GameIcons.destroyedShip;
                    currentPlayer.SetIsPlayerTurn(true);
                }
                else if (cell == GameIcons.emptyCell)
                {
                    cell = GameIcons.damagedCell;
                    currentPlayer.SetIsPlayerTurn(false);
                    otherPlayer.SetIsPlayerTurn(true);
                }
            }

            return cell;
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