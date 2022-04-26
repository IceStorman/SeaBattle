using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle
{
    public class InitialArrangement
    {
        private static int numberOfCells = 10;

        private static char wall = '#';
        private static char emptyCell = ' ';

        public static char[,] FieldGenerating()
        {
            char[,] field = new char[numberOfCells, numberOfCells];
            char cell = emptyCell;

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
                        else if (j < numberOfCells - 1) cell = emptyCell;
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

        public static void FieldDrawing(char[,] field)
        {
            char cell = emptyCell;

            for(int i = 0; i < numberOfCells; i++)
            {
                for(int j = 0; j < numberOfCells; j++)
                {
                    cell = field[j, i];
                    Console.Write(cell);
                }
                Console.WriteLine();
            }
        }
    }
}
