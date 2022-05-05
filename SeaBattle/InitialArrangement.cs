using System;

namespace SeaBattle
{
    public class InitialArrangement
    {
        private const int numberOfCells = 10;
        private const int numberOfShips = 10;

        public static void FieldGenerating(Player player)
        {
            Random rnd = new Random();

            char[,] field = new char[numberOfCells, numberOfCells];
            char cell = GameIcons.emptyCell;

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
                        else if (j < numberOfCells - 1) cell = SpawnShips(player, rnd);
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

        private static char SpawnShips(Player player, Random rnd)
        {
            int num = rnd.Next(0, 4);

            char cell = GameIcons.emptyCell;

            if(num <= 0 && player.numberOfShips < numberOfShips)
            {
                cell = GameIcons.ship;
                player.numberOfShips++;
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