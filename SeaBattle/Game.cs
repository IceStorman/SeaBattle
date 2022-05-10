using System;

namespace SeaBattle
{
    internal class Game
    {
        private const ConsoleKey shootKey = ConsoleKey.Enter;

        private const int numberOfCells = 8;
        private const int numberOfCellsOnAllField = numberOfCells + 2;
        private const int maxNumberOfShips = 10;

        public void Play()
        {
            Player player1 = new Player();
            Player player2 = new Player();

            SetStartParameters(player1);
            SetStartParameters(player2);

            FieldGenerating(player1);
            FieldGenerating(player2);

            while (!IsGameEnd(player1.numberOfShips, player2.numberOfShips))
            {
                FieldDrawing(player1);
                FieldDrawing(player2);

                Console.WriteLine("Player1 ships: " + player1.numberOfShips);
                Console.WriteLine("Player2 ships: " + player2.numberOfShips);

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
            EndGameMessage(player1.numberOfShips, player2.numberOfShips);

            Console.ReadKey();
        }

        private void SetStartParameters(Player player)
        {
            player.xPos = 1;
            player.yPos = 1;
        }

        private void FieldGenerating(Player player)
        {
            Random rnd = new Random();

            char[,] field = new char[numberOfCellsOnAllField, numberOfCellsOnAllField];

            for (int i = 0; i < numberOfCellsOnAllField; i++)
            {
                for (int j = 0; j < numberOfCellsOnAllField; j++)
                {
                    char cell = (char)GameIcons.Icons.emptyCell;

                    if (i == 0)
                    {
                        if (j < numberOfCellsOnAllField - 1) 
                            cell = Convert.ToChar(j.ToString());
                        else 
                            cell = (char)GameIcons.Icons.wall;
                    }
                    else if (i < numberOfCellsOnAllField - 1)
                    {
                        if (j == 0) 
                            cell = Convert.ToChar(i.ToString());
                        else if (j < numberOfCellsOnAllField - 1) 
                            cell = SpawnShips(player, rnd);
                        else 
                            cell = (char)GameIcons.Icons.wall;
                    }
                    else
                    {
                        cell = (char)GameIcons.Icons.wall;
                    }

                    field[j, i] = cell;
                }
            }
            player.field = field;
        }

        private char SpawnShips(Player player, Random rnd)
        {
            int num = rnd.Next(0, 4);

            char cell = (char)GameIcons.Icons.emptyCell;

            if (num <= 0 && player.numberOfShips < maxNumberOfShips)
            {
                cell = (char)GameIcons.Icons.ship;
                player.numberOfShips++;
            }
            return cell;
        }

        private void FieldDrawing(Player player)
        {
            for (int i = 0; i < numberOfCellsOnAllField; i++)
            {
                for (int j = 0; j < numberOfCellsOnAllField; j++)
                {
                    char cell = (char)GameIcons.Icons.emptyCell;

                    if (j == player.xPos && i == player.yPos) 
                        cell = (char)GameIcons.Icons.playerCell;
                    else if (player.field[j, i] == (char)GameIcons.Icons.ship) 
                        cell = (char)GameIcons.Icons.emptyCell;
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

            if (CanMove(currentPlayer.field, newX, newY))
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
                if (cell == (char)GameIcons.Icons.ship)
                {
                    cell = (char)GameIcons.Icons.destroyedShip;
                    currentPlayer.isPlayerTurn = true;
                }
                else if (cell == (char)GameIcons.Icons.emptyCell)
                {
                    cell = (char)GameIcons.Icons.damagedCell;
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
                if (player.field[player.xPos, player.yPos] == (char)GameIcons.Icons.ship)
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

        private bool CanMove(char[,] field, int newX, int newY)
        {
            if (field[newX, newY] == (char)GameIcons.Icons.emptyCell || field[newX, newY] == (char)GameIcons.Icons.ship || field[newX, newY] == (char)GameIcons.Icons.damagedCell || field[newX, newY] == (char)GameIcons.Icons.destroyedShip)
            {
                return true;
            }
            return false;
        }

        private void Move(Player player, int newX, int newY)
        {
            player.xPos = newX;
            player.yPos = newY;
        }

        private bool IsGameEnd(int numberOfPlayer1Ships, int numberOfPlayer2Ships)
        {
            if (numberOfPlayer1Ships <= 0 || numberOfPlayer2Ships <= 0) return true;
            return false;
        }

        private void EndGameMessage(int numberOfPlayer1Ships, int numberOfPlayer2Ships)
        {
            if (numberOfPlayer1Ships <= 0)
            {
                Console.WriteLine("Player2 won!");
            }
            else if (numberOfPlayer2Ships <= 0)
            {
                Console.WriteLine("Player1 won!");
            }
        }
    }
}