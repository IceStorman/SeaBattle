using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle
{
    public class InitialArrangement
    {
        private static Random rnd = new Random();

        private static int numberOfCells = 10;
        private static int countOfShips = 0;

        private static char wall = '#';
        private static char emptyCell = ' ';
        private static char playerCell = '*';
        private static char ship = '+';

        public static char[,] FieldGenerating(int numberOfShips)
        {
            char[,] field = new char[numberOfCells, numberOfCells];
            char cell = emptyCell;
            countOfShips = 0;

            for(int i = 0; i < numberOfCells; i++)
            {
                for(int j = 0; j < numberOfCells; j++)
                {
                    if(i == 0)
                    {
                        if (j < numberOfCells - 1) cell = Convert.ToChar(j.ToString());
                        else cell = wall;
                    }
                    else if(i < numberOfCells - 1)
                    {
                        if (j == 0) cell = Convert.ToChar(i.ToString());
                        else if (j < numberOfCells - 1) cell = ShipGeneration(numberOfShips);
                        else cell = wall;
                    }
                    else
                    {
                        cell = wall;
                    }

                    field[j, i] = cell;
                }
            }
            return field;
        }

        private static char ShipGeneration(int numberOfShips)
        {
            int num = rnd.Next(0, 100);
            char cell = emptyCell;

            if(num <= 25 && countOfShips < numberOfShips)
            {
                cell = ship;
                countOfShips++;
            }
            return cell;
        }

        public static void FieldDrawing(char[,] field, int playerX, int playerY)
        {
            char cell = emptyCell;

            for(int i = 0; i < numberOfCells; i++)
            {
                for(int j = 0; j < numberOfCells; j++)
                {
                    if (j == playerX && i == playerY) cell = playerCell;
                    else if (field[j, i] == ship) cell = emptyCell;
                    else cell = field[j, i];

                    Console.Write(cell);
                }
                Console.WriteLine();
            }
        }
    }
}
