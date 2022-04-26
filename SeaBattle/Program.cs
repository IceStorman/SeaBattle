using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle
{
    internal class Program
    {
        private static char[,] firstField = InitialArrangement.FieldGenerating();
        private static char[,] secondField = InitialArrangement.FieldGenerating();

        static void Main(string[] args)
        {
            while (!IsGameEnd())
            {
                InitialArrangement.FieldDrawing(firstField);
                InitialArrangement.FieldDrawing(secondField);
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static bool IsGameEnd()
        {
            return false;
        }
    }
}
