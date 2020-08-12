using System;

namespace BlindMaze
{
    //Main program where all necessary classes and functions are called to run the minigame.
    class Program
    {
        static Maze gamingMaze;
        static MazeHandler mazeHandler = new MazeHandler();
        static Player newPlayer = new Player();
        static PlayerHandler playerHandler = new PlayerHandler();
        static DefeatMazeHandler defeatMazeHandler = new DefeatMazeHandler();
        static void Main(string[] args)
        {
            int[] mazeDims = new int[2];
            int userDifficulty = mazeHandler.AskDifficulty();
            mazeDims = mazeHandler.SetMazeSize(userDifficulty);
            gamingMaze = mazeHandler.GenerateMaze(mazeDims);            
            mazeHandler.SetExitPosition(newPlayer, gamingMaze);
            mazeHandler.HideTheTraps(gamingMaze);
            Console.WriteLine("\n You've got 3 lives to try and find the way out.");
            Console.WriteLine("Use the arrow keys to move through the maze, but be careful with traps! \n");
            mazeHandler.PrintMaze(gamingMaze, newPlayer);
            playerHandler.PlayTheGame(gamingMaze, newPlayer, mazeHandler);
            defeatMazeHandler.PrintMaze(gamingMaze, newPlayer);
            Console.ReadLine();
        }
    }
                                                                                                                             
}
