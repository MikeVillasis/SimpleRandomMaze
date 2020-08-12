using System;
using System.Collections.Generic;
using System.Text;

namespace BlindMaze
{
    //This class handles player's movements on the board
    public class PlayerHandler
    {
        public bool MovePlayer(Player currentPlayer, int moveDirection, Maze currentMaze)
        {
            bool playerMoved = false;
            currentPlayer.CheckPossibleMovements(currentPlayer, currentMaze);
            bool isATrap = false;

            switch (moveDirection)
            {
                case 0:
                    if (currentPlayer.canMoveUp)
                    {
                        if (currentMaze.currentCells[currentPlayer.mPosition - 1, currentPlayer.nPosition].Trap)
                        {
                            isATrap = true;
                            currentMaze.currentCells[currentPlayer.mPosition - 1, currentPlayer.nPosition].isCellVisited = true;
                        }
                        else
                        {
                            currentMaze.currentCells[currentPlayer.mPosition, currentPlayer.nPosition].isPlayerCell = false;
                            currentMaze.currentCells[currentPlayer.mPosition - 1, currentPlayer.nPosition].isPlayerCell = true;
                            currentMaze.currentCells[currentPlayer.mPosition, currentPlayer.nPosition].isCellVisited = true;
                            currentPlayer.mPosition--;
                        }
                        
                        playerMoved = true;
                    }
                    break;
                case 1:
                    if (currentPlayer.canMoveRight)
                    {
                        if (currentMaze.currentCells[currentPlayer.mPosition, currentPlayer.nPosition + 1].Trap)
                        {
                            isATrap = true;
                            currentMaze.currentCells[currentPlayer.mPosition, currentPlayer.nPosition + 1].isCellVisited = true;
                        }
                        else
                        {
                            currentMaze.currentCells[currentPlayer.mPosition, currentPlayer.nPosition].isPlayerCell = false;
                            currentMaze.currentCells[currentPlayer.mPosition, currentPlayer.nPosition + 1].isPlayerCell = true;
                            currentMaze.currentCells[currentPlayer.mPosition, currentPlayer.nPosition].isCellVisited = true;
                            currentPlayer.nPosition++;
                        }                        
                        playerMoved = true;
                    }
                    break;
                case 2:
                    if (currentPlayer.canMoveLeft)
                    {
                        if (currentMaze.currentCells[currentPlayer.mPosition, currentPlayer.nPosition - 1].Trap)
                        {
                            isATrap = true;
                            currentMaze.currentCells[currentPlayer.mPosition, currentPlayer.nPosition - 1].isCellVisited = true;
                        }
                        else
                        {
                            currentMaze.currentCells[currentPlayer.mPosition, currentPlayer.nPosition].isPlayerCell = false;
                            currentMaze.currentCells[currentPlayer.mPosition, currentPlayer.nPosition - 1].isPlayerCell = true;
                            currentMaze.currentCells[currentPlayer.mPosition, currentPlayer.nPosition].isCellVisited = true;
                            currentPlayer.nPosition--;
                        }
 
                        playerMoved = true;
                    }
                    break;
                case 3:
                    if (currentPlayer.canMoveDown)
                    {
                        if (currentMaze.currentCells[currentPlayer.mPosition + 1, currentPlayer.nPosition].Trap)
                        {
                            isATrap = true;
                            currentMaze.currentCells[currentPlayer.mPosition + 1, currentPlayer.nPosition].isCellVisited = true;
                        }
                        else
                        {
                            currentMaze.currentCells[currentPlayer.mPosition, currentPlayer.nPosition].isPlayerCell = false;
                            currentMaze.currentCells[currentPlayer.mPosition + 1, currentPlayer.nPosition].isPlayerCell = true;
                            currentMaze.currentCells[currentPlayer.mPosition, currentPlayer.nPosition].isCellVisited = true;
                            currentPlayer.mPosition++;
                        }                        
                        playerMoved = true;
                    }
                    break;

            }
            if(playerMoved)
            {
                if(isATrap)
                {
                    currentPlayer.playerLives--;
                    if (currentPlayer.playerLives > 1)
                    {
                        Console.Clear();
                        Console.WriteLine("You fell in a trap and lost a life! You have " + currentPlayer.playerLives + " lives remaining");
                        Console.WriteLine("Press any button to try again. Be careful!");
                        Console.ReadLine();
                    }
                    else if(currentPlayer.playerLives == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("You fell in a trap and lost a life! You have " + currentPlayer.playerLives + " lives remaining");
                        Console.WriteLine("This is your last life! We'll show you the exit, try and get there now!");
                        Console.WriteLine("Press any button to try again. Be careful!");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("You lost! Better luck next time");
                        Console.ReadLine();
                    }
                        
                }
            }
            return playerMoved;
        }

        public bool PlayerFoundExit(Maze currentMaze)
        {
            bool isPlayerInExit = false;
            int mExitCoor = 0;
            int nExitCoor = 0;
            int mPlayerCoor = 0;
            int nPlayerCoor = 0;

            for (int m = 0; m < currentMaze.mDimension; m++)
            {
                for (int n = 0; n < currentMaze.nDimension; n++)
                {
                    if (currentMaze.currentCells[m, n].isPlayerCell)
                    {
                        mPlayerCoor = m;
                        nPlayerCoor = n;
                    }
                    if (currentMaze.currentCells[m, n].isExit)
                    {
                        mExitCoor = m;
                        nExitCoor = n;
                    }

                }
            }
            if(mPlayerCoor == mExitCoor && nPlayerCoor == nExitCoor)
            {
                isPlayerInExit = true;
            }

            return isPlayerInExit;
        }

        public void PlayTheGame(Maze currentMaze, Player currentPlayer, MazeHandler mazeHandler)
        {
            ConsoleKeyInfo inputKey;
            bool isMovementValid = false;
            bool isExitFound = false;
            int playerMoveDir = 0;
            do
            {
                do
                {
                    do
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\n Use the arrow keys ONLY to change position: ");
                        inputKey = Console.ReadKey();
                    } while (inputKey.Key != ConsoleKey.RightArrow && inputKey.Key != ConsoleKey.LeftArrow
                         && inputKey.Key != ConsoleKey.UpArrow && inputKey.Key != ConsoleKey.DownArrow);

                    currentPlayer.CheckPossibleMovements(currentPlayer, currentMaze);
                    switch (inputKey.Key)
                    {
                        case ConsoleKey.UpArrow:
                            playerMoveDir = 0;
                            break;
                        case ConsoleKey.RightArrow:
                            playerMoveDir = 1;
                            break;
                        case ConsoleKey.LeftArrow:
                            playerMoveDir = 2;
                            break;
                        case ConsoleKey.DownArrow:
                            playerMoveDir = 3;
                            break;
                    }
                    isMovementValid = MovePlayer(currentPlayer, playerMoveDir, currentMaze);
                    if (!isMovementValid)
                        Console.WriteLine("YOU CANNOT GO OFF THE MAZE!");

                } while (!isMovementValid);
                Console.Clear();
                mazeHandler.PrintMaze(currentMaze, currentPlayer);
                if (currentPlayer.playerLives == 0)
                    break;
                isExitFound = PlayerFoundExit(currentMaze);
            } while (!isExitFound);
            if (isExitFound)
            {
                Console.WriteLine("GG's! You found the exit");
                Console.WriteLine("Press any key to exit the program");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Unlucky! Restart the program if you wish to try again");
                Console.WriteLine("Press any key to exit the program");
                Console.ReadLine();
            }

        }

    }
    
}
    

