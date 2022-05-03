using System;

namespace SeaBattle
{
    public class InitialArrangement
    {
        private static Random rnd = new Random();

        private static int numberOfCells = 10;
        private static int countOfShips = 0;

        public static void FieldGenerating(Player player)
        {
            char[,] field = new char[numberOfCells, numberOfCells];
            char cell = GameIcons.emptyCell;
            countOfShips = 0;

            for(int i = 0; i < numberOfCells; i++)
            {
                for(int j = 0; j < numberOfCells; j++)
                {
                    if(i == 0)
                    {
                        if (j < numberOfCells - 1) cell = Convert.ToChar(j.ToString());
                        else cell = GameIcons.wall;
                    }
                    else if(i < numberOfCells - 1)
                    {
                        if (j == 0) cell = Convert.ToChar(i.ToString());
                        else if (j < numberOfCells - 1) cell = SpawnShips(player.numberOfShips);
                        else cell = GameIcons.wall;
                    }
                    else
                    {
                        cell = GameIcons.wall;
                    }

                    field[j, i] = cell;
                }
            }
            player.SetField(field);
        }

        private static char SpawnShips(int numberOfShips)
        {
            int num = rnd.Next(0, 100);
            char cell = GameIcons.emptyCell;

            if(num <= 25 && countOfShips < numberOfShips)
            {
                cell = GameIcons.ship;
                countOfShips++;
            }
            return cell;
        }

        public static void FieldDrawing(Player player)
        {
            char cell = GameIcons.emptyCell;

            for(int i = 0; i < numberOfCells; i++)
            {
                for(int j = 0; j < numberOfCells; j++)
                {
                    if (j == player.xPos && i == player.yPos) cell = GameIcons.playerCell;
                    else if (player.field[j, i] == GameIcons.ship) cell = GameIcons.emptyCell;
                    else cell = player.field[j, i];

                    Console.Write(cell);
                }
                Console.WriteLine();
            }
        }
    }
}
