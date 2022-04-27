using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle
{
    public class MovementAcrossTheField
    {
        private static char emptyCell = ' ';
        private static char damagedCell = '-';
        private static char destroyedShip = '=';
        private static char ship = '+';

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

        public static (int, int) MoveLogic(int dx, int dy, int playerX, int playerY)
        {
            int newX = playerX + dx;
            int newY = playerY + dy;

            return (newX, newY);
        }

        public static bool CanMove(char[,] field, int newX, int newY)
        {
            if(field[newX, newY] == emptyCell || field[newX, newY] == ship || field[newX, newY] == damagedCell || field[newX, newY] == destroyedShip)
            {
                return true;
            }
            return false;
        }

        public static (int, int) Move(int newX, int newY)
        {
            int playerX = newX;
            int playerY = newY;

            return (playerX, playerY);
        }
    }
}
