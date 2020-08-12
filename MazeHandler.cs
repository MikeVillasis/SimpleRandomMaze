using System;
using System.Collections.Generic;
using System.Text;

namespace BlindMaze
{
    //This class handles all main funcionalities to play the game with the Maze and how it changes throughout
    //each step of the way, from creation to Victory/Failure
    public class MazeHandler
    {
        public int AskDifficulty()
        {
            int answer = 0;
            int outNumber = 0;
            do
            {
                do
                {
                    Console.WriteLine("Welcome, Player. Your solitary journey starts today.");
                    Console.WriteLine("The maze is based on a 4-8 difficulty. \n However, you do not control your luck," +
                        " so just choose a number to make you feel better");
                } while (!Int32.TryParse(Console.ReadLine(), out outNumber));
                answer = outNumber;               
            } while (answer <= 3 || answer >=9);

            return outNumber;
        }

        public int[] SetMazeSize(int userDiff)
        {
            Random random = new Random();
            int[] mazeDims = new int[2];
            int randomDim = 0;
            int maxDiff = userDiff;

            if(userDiff %2 == 0)
            {
                randomDim = random.Next(2, maxDiff);
                mazeDims[0] = userDiff;
                mazeDims[1] = randomDim;
            }
            else
            {
                randomDim = random.Next(2, maxDiff);
                mazeDims[0] = randomDim;
                mazeDims[1] = userDiff;
            }
            return mazeDims;
        }
        public virtual void PrintMaze(Maze currentMaze, Player currentPlayer)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.Write("\t");
            for (int i = 0; i < currentMaze.nDimension; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                if (i == 0)
                    Console.Write("   [" + i + "]");
                else
                    Console.Write("[" + i + "]");
            }
            Console.WriteLine("");
            for (int m = 0; m < currentMaze.mDimension; m++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\t[" + m + "]");
                for (int n = 0; n < currentMaze.nDimension; n++)
                {
                    if (m == currentPlayer.mPosition && n == currentPlayer.nPosition)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("[֎]");
                    }
                    else if (currentMaze.currentCells[m, n].isExit)
                    {
                        if(currentPlayer.playerLives == 1)
                            Console.ForegroundColor = ConsoleColor.Magenta;
                        else
                            Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("[֎]");
                    }
                    else
                    {
                        if (currentMaze.currentCells[m,n].isCellVisited)
                        {
                            if(currentMaze.currentCells[m, n].Trap)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("[֎]");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("[֎]");
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("[֎]");
                        }
                    }

                }
                Console.WriteLine("");
            }
        }
        public Maze GenerateMaze(int[] mazeDimensions)
        {
            Maze generatedMaze = new Maze(mazeDimensions[0], mazeDimensions[1]);
            int mDim = mazeDimensions[0];
            int nDim = mazeDimensions[1];
            generatedMaze.mDimension = mDim;
            generatedMaze.nDimension = nDim;

            for (int m = 0; m < mDim; m++)
            {
                for (int n = 0; n < nDim; n++)
                {
                    generatedMaze.currentCells[m, n]._mCoordinate = m;
                    generatedMaze.currentCells[m, n]._nCoordinate = n;
                }
            }
            generatedMaze.currentCells[0, 0].isCornerCell = true;
            generatedMaze.currentCells[mDim - 1, 0].isCornerCell = true;
            generatedMaze.currentCells[0, nDim - 1].isCornerCell = true;
            generatedMaze.currentCells[mDim - 1, nDim - 1].isCornerCell = true;
            return generatedMaze;
        }
      
        public void SetExitPosition(Player currentPlayer, Maze currentMaze)
        {
            int mDim = currentMaze.mDimension;
            int nDim = currentMaze.nDimension;
            Random rng = new Random();
            int rngNumber = rng.Next(0, 4);
            int mPos = 0;
            int nPos = 0;
            Maze.MazeCell provExitCell = new Maze.MazeCell();
                switch (rngNumber)
                {
                    //Top Edge starting point
                    case 0:
                        mPos = 0;
                        nPos = rng.Next(0, nDim);
                        break;
                    //Right Edge starting point
                    case 1:
                        mPos = rng.Next(0, mDim);
                        nPos = nDim;
                        break;
                    //Bottom Edge starting point
                    case 2:
                        mPos = mDim;
                        nPos = rng.Next(0, nDim);
                        break;
                    //Left Edge starting point
                    case 3:
                        mPos = rng.Next(0, mDim);
                        nPos = 0;
                        break;
                }
                if (mPos == mDim)
                    mPos--;
                if (nPos == nDim)
                    nPos--;

            currentMaze.currentCells[mPos, nPos].isExit = true;
            provExitCell = currentMaze.currentCells[mPos, nPos]; 
            SetMinPath(currentMaze, provExitCell, currentPlayer);

        }
        static void SetMinPath(Maze currentMaze, Maze.MazeCell currentExit, Player currentPlayer)
        {
            Maze.MazeCell provExitCell = currentExit;
            List<Maze.MazeCell> currentCellNeighbours = GetCellNeighbours(provExitCell, currentMaze);
            Maze.MazeCell currentCell = new Maze.MazeCell();
            bool isPathSet = false;
            int changedMPos = 0;
            int changedNPos = 0;
            int minDistance = 0;
            int newNonTrapNeighbour = 0;
            Random random = new Random();
            int elapsedTime = 0;
            while(!isPathSet)
            {
                elapsedTime++;
                foreach (Maze.MazeCell mazeCell in currentCellNeighbours)
                {
                    do
                    {
                        newNonTrapNeighbour = random.Next(0, currentCellNeighbours.Count);
                        changedMPos = currentCellNeighbours[newNonTrapNeighbour]._mCoordinate;
                        changedNPos = currentCellNeighbours[newNonTrapNeighbour]._nCoordinate;

                    } while(currentMaze.currentCells[changedMPos, changedNPos].Trap 
                            || currentMaze.currentCells[changedMPos, changedNPos].isCellVisited);
                    
                    currentMaze.currentCells[changedMPos, changedNPos].Trap = false;
                    currentMaze.currentCells[changedMPos, changedNPos].isCellVisited = true;

                    if ((currentMaze.currentCells[changedMPos, changedNPos].isCornerCell ||
                        currentMaze.currentCells[changedMPos, changedNPos].isBorderCell) && minDistance > 1)
                    {
                        isPathSet = true;
                        currentMaze.currentCells[changedMPos, changedNPos].isPlayerCell = true;
                        currentPlayer.mPosition = changedMPos;
                        currentPlayer.nPosition = changedNPos;
                        break;
                    }
                    minDistance++;
                    currentCell = currentMaze.currentCells[changedMPos, changedNPos];
                    currentCellNeighbours = GetCellNeighbours(currentCell, currentMaze);
                }
            }         

        }

        static List<Maze.MazeCell> GetCellNeighbours(Maze.MazeCell currentCell, Maze currentMaze)
        {
            List<Maze.MazeCell> provCellList = new List<Maze.MazeCell>();
            int currentNeighbours = 0;
            int mPos = currentCell._mCoordinate;
            int nPos = currentCell._nCoordinate;
            if(mPos - 1 >= 0)
            {
                Maze.MazeCell provCell = currentMaze.currentCells[mPos - 1, nPos];
                provCellList.Add(provCell);
                currentNeighbours++;
            }
            if (mPos + 1 < currentMaze.mDimension)
            {
                Maze.MazeCell provCell = currentMaze.currentCells[mPos + 1, nPos];
                provCellList.Add(provCell);
                currentNeighbours++;
            }
            if (nPos - 1 >= 0)
            {
                Maze.MazeCell provCell = currentMaze.currentCells[mPos, nPos - 1];
                provCellList.Add(provCell);
                currentNeighbours++;
            }
            if (nPos + 1 < currentMaze.nDimension)
            {
                Maze.MazeCell provCell = currentMaze.currentCells[mPos, nPos + 1];
                provCellList.Add(provCell);
                currentNeighbours++;
            }


            return provCellList;
        }

        public void HideTheTraps(Maze currentMaze)
        {
            int numberOfCells = (currentMaze.mDimension * currentMaze.nDimension);
            int mDimension = currentMaze.mDimension;
            int nDimension = currentMaze.nDimension;
            Random random = new Random();
            int randomMTrapIndex = 0;
            int randomNTrapIndex = 0;
            int maxTrapAttemps = 0;
            while(maxTrapAttemps < 20)
            {
                randomMTrapIndex = random.Next(0, mDimension);
                randomNTrapIndex = random.Next(0, nDimension);
                if (!currentMaze.currentCells[randomMTrapIndex, randomNTrapIndex].isCellVisited
                    && !currentMaze.currentCells[randomMTrapIndex, randomNTrapIndex].isExit
                    && !currentMaze.currentCells[randomMTrapIndex, randomNTrapIndex].isPlayerCell)
                    currentMaze.currentCells[randomMTrapIndex, randomNTrapIndex].Trap = true;

                maxTrapAttemps++;
            }

            for (int m = 0; m < currentMaze.mDimension; m++)
            {
                for (int n = 0; n < currentMaze.nDimension; n++)
                {
                    if (!currentMaze.currentCells[m,n].isExit && !currentMaze.currentCells[m, n].isPlayerCell)
                    {
                        currentMaze.currentCells[m, n].isCellVisited = false;
                    }

                }
            }
        }


    }
}
