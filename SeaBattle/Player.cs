namespace SeaBattle
{
    public class Player
    {
        public char[,] field;

        public void SetField(char[,] field)
        {
            this.field = field;
        }

        public char[,] GetField()
        {
            return field;
        }

        public int xPos;

        public void SetXPos(int xPos)
        {
            this.xPos = xPos;
        }

        public int GetXPos()
        {
            return xPos;
        }

        public int yPos;

        public void SetYPos(int yPos)
        {
            this.yPos = yPos;
        }

        public int GetYPos()
        {
            return yPos;
        }

        public int numberOfShips;

        public void SetNumberOfShips(int numberOfShips)
        {
            this.numberOfShips = numberOfShips;
        }

        public int GetNumberOfShips()
        {
            return numberOfShips;
        }
    }
}
