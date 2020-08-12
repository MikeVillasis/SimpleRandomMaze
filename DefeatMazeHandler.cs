using System;
using System.Collections.Generic;
using System.Text;

namespace BlindMaze
{
    //This class overrides the PrintMaze method to get the Losing Screen Easter Egg
    public class DefeatMazeHandler : MazeHandler
    {
        public override void PrintMaze(Maze currentMaze, Player currentPlayer)
        {
            if(currentPlayer.playerLives == 0)
            {
                Console.Clear();
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                Console.Write("\t Take the \n \t");
                for (int i = 0; i < 5; i++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    if (i == 0)
                        Console.Write("   [" + i + "]");
                    else
                        Console.Write("[" + i + "]");
                }
                Console.WriteLine("");
                for (int m = 0; m < 5; m++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\t[" + m + "]");
                    for (int n = 0; n < 5; n++)
                    {
                        if ((m == 0 && n == 1) || (m == 1 && n == 1) || (m == 2 && n == 1) || (m == 3 && n == 1)
                            || (m == 4 && n == 1) || (m == 4 && n == 2) || (m == 4 && n == 3))
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        else
                            Console.ForegroundColor = ConsoleColor.DarkGray;

                        Console.Write("[֎]");

                    }
                    Console.WriteLine("");
                }
            }

        }
    }
}
