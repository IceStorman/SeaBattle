using System;

namespace SeaBattle
{
    public class MovementAcrossTheField
    {
        public static (int, int) MovingInput(ConsoleKeyInfo key)
        {
            int dx = 0;
            int dy = 0;

            if (key.Key == ConsoleKey.UpArrow) dy = -1;
            else if(key.Key == ConsoleKey.DownArrow) dy = 1;
            else if(key.Key == ConsoleKey.LeftArrow) dx = -1;
            else if( key.Key == ConsoleKey.RightArrow) dx = 1;

            return (dx, dy);
        }

        public static (int, int) MoveLogic(int dx, int dy, Player player)
        {
            int newX = player.xPos + dx;
            int newY = player.yPos + dy;

            return (newX, newY);
        }

        public static bool CanMove(char[,] field, int newX, int newY)
        {
            if(field[newX, newY] == GameIcons.emptyCell || field[newX, newY] == GameIcons.ship || field[newX, newY] == GameIcons.damagedCell || field[newX, newY] == GameIcons.destroyedShip)
            {
                return true;
            }
            return false;
        }

        public static void Move(Player player, int newX, int newY)
        {
            player.SetXPos(newX);
            player.SetYPos(newY);
        }
    }
}