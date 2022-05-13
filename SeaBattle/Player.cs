namespace SeaBattle
{
    public class Player
    {
        public string name;
        public char[,] field;
        public int xPos;
        public int yPos;
        public int numberOfShips;
        public bool isPlayerTurn = true;
        public int countOfWins;
    }
}