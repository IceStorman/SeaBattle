using System;

namespace SeaBattle
{
    internal class Game
    {
        private const ConsoleKey shootKey = ConsoleKey.Enter;

        private const int numberOfCells = 8;
        private const int numberOfCellsOnAllField = numberOfCells + 2;
        private const int maxNumberOfShips = 10;

        public void StartRound(Player player1, Player player2)
        {
            SetStartParameters(player1);
            SetStartParameters(player2);

            FieldGenerating(player1);
            FieldGenerating(player2);

            while (!IsRoundEnd(player1, player2))
            {
                FieldDrawing(player1);
                FieldDrawing(player2);

                Console.WriteLine($"{player1.name} ships: {player1.numberOfShips}");
                Console.WriteLine($"{player2.name} ships: {player2.numberOfShips}");

                ConsoleKeyInfo key = Console.ReadKey();

                (int dx, int dy) = MovingInput(key);

                if (player1.isPlayerTurn)
                {
                    Move(player1, player2, key, dx, dy);
                }
                else if (player2.isPlayerTurn)
                {
                    Move(player2, player1, key, dx, dy);
                }

                Console.Clear();
            }
            EndRoundLogic(player1, player2);

            Console.ReadKey();
            Console.Clear();
        }

        private void SetStartParameters(Player player)
        {
            player.xPos = 1;
            player.yPos = 1;
            player.numberOfShips = 0;
        }

        private void FieldGenerating(Player player)
        {
            Random rnd = new Random();

            char[,] field = new char[numberOfCellsOnAllField, numberOfCellsOnAllField];

            for (int i = 0; i < numberOfCellsOnAllField; i++)
            {
                for (int j = 0; j < numberOfCellsOnAllField; j++)
                {
                    char cell = (char)GameIcons.emptyCell;

                    if (i == 0)
                    {
                        if (j < numberOfCellsOnAllField - 1) 
                            cell = Convert.ToChar(j.ToString());
                        else 
                            cell = (char)GameIcons.wall;
                    }
                    else if (i < numberOfCellsOnAllField - 1)
                    {
                        if (j == 0) 
                            cell = Convert.ToChar(i.ToString());
                        else if (j < numberOfCellsOnAllField - 1) 
                            cell = SpawnShips(player, rnd);
                        else 
                            cell = (char)GameIcons.wall;
                    }
                    else
                    {
                        cell = (char)GameIcons.wall;
                    }

                    field[j, i] = cell;
                }
            }
            player.field = field;
        }

        private char SpawnShips(Player player, Random rnd)
        {
            int num = rnd.Next(0, 4);

            char cell = (char)GameIcons.emptyCell;

            if (num == 0 && player.numberOfShips < maxNumberOfShips)
            {
                cell = (char)GameIcons.ship;
                player.numberOfShips++;
            }
            return cell;
        }

        private void FieldDrawing(Player player)
        {
            Console.WriteLine(player.name);

            for (int i = 0; i < numberOfCellsOnAllField; i++)
            {
                for (int j = 0; j < numberOfCellsOnAllField; j++)
                {
                    char cell = (char)GameIcons.emptyCell;

                    if (j == player.xPos && i == player.yPos) 
                        cell = (char)GameIcons.playerCell;
                    else if (player.field[j, i] == (char)GameIcons.ship) 
                        cell = (char)GameIcons.ship;
                    else 
                        cell = player.field[j, i];

                    Console.Write(cell);
                }
                Console.WriteLine();
            }
        }

        private void Move(Player currentPlayer, Player otherPlayer, ConsoleKeyInfo key, int dx, int dy)
        {
            CountOfShips(key, currentPlayer);

            (int newX, int newY) = MoveLogic(dx, dy, currentPlayer);

            if (CanMove(currentPlayer.field[newX, newY]))
            {
                Move(currentPlayer, newX, newY);
            }

            currentPlayer.field[currentPlayer.xPos, currentPlayer.yPos] = Shoot(key, currentPlayer, otherPlayer);
        }

        private char Shoot(ConsoleKeyInfo key, Player currentPlayer, Player otherPlayer)
        {
            char cell = currentPlayer.field[currentPlayer.xPos, currentPlayer.yPos];

            if (key.Key == shootKey)
            {
                if (cell == (char)GameIcons.ship)
                {
                    cell = (char)GameIcons.destroyedShip;
                    currentPlayer.isPlayerTurn = true;
                }
                else if (cell == (char)GameIcons.emptyCell)
                {
                    cell = (char)GameIcons.damagedCell;
                    currentPlayer.isPlayerTurn = false;
                    otherPlayer.isPlayerTurn = true;
                }
            }

            return cell;
        }

        private void CountOfShips(ConsoleKeyInfo key, Player player)
        {
            int num = player.numberOfShips;
            if (key.Key == shootKey)
            {
                if (player.field[player.xPos, player.yPos] == (char)GameIcons.ship)
                {
                    num--;
                }
            }
            player.numberOfShips = num;
        }

        private (int, int) MovingInput(ConsoleKeyInfo key)
        {
            int dx = 0;
            int dy = 0;

            if (key.Key == ConsoleKey.UpArrow) 
                dy = -1;
            else if (key.Key == ConsoleKey.DownArrow) 
                dy = 1;
            else if (key.Key == ConsoleKey.LeftArrow) 
                dx = -1;
            else if (key.Key == ConsoleKey.RightArrow) 
                dx = 1;

            return (dx, dy);
        }

        private (int, int) MoveLogic(int dx, int dy, Player player)
        {
            int newX = player.xPos + dx;
            int newY = player.yPos + dy;

            return (newX, newY);
        }

        private bool CanMove(char fieldXY)
        {
            switch (fieldXY)
            {
                case (char)GameIcons.emptyCell:
                case (char)GameIcons.ship:
                case (char)GameIcons.damagedCell:
                case (char)GameIcons.destroyedShip:
                    return true;
                default:
                    return false;
            }
        }

        private void Move(Player player, int newX, int newY)
        {
            player.xPos = newX;
            player.yPos = newY;
        }

        private bool IsRoundEnd(Player player1, Player player2)
        {
            if (player1.numberOfShips <= 0 
                || player2.numberOfShips <= 0) 
                return true;
            return false;
        }

        private void EndRoundLogic(Player player1, Player player2)
        {
            if (player1.numberOfShips <= 0)
            {
                Console.WriteLine(player2.name + " won this round!");
                player2.countOfWins++;
            }
            else if (player2.numberOfShips <= 0)
            {
                Console.WriteLine(player1.name + " won this round!");
                player1.countOfWins++;
            }
            Console.WriteLine();
            Console.WriteLine("Count now:");
            Console.WriteLine($"{player1.name}: {player1.countOfWins}");
            Console.WriteLine($"{player2.name}: {player2.countOfWins}");
            Console.WriteLine("Press any key, if you want continue...");
        }
    }
}