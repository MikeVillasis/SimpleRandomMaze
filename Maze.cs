using System;
using System.Collections.Generic;
using System.Text;

namespace BlindMaze
{
    public class Maze
    {
        public MazeCell[,] currentCells;
        public int mDimension;
        public int nDimension;

        public Maze(int mCells, int nCells)
        {
            MazeCell[,] provisionalCellsArray = new MazeCell[mCells, nCells];
            this.currentCells = provisionalCellsArray;
        }

        public struct MazeCell
        {            
            public bool isExit;
            public bool isPlayerCell;
            public bool isPlayerIn;
            public bool isBorderCell;
            public bool isCornerCell;
            public bool isCellVisited;
            public int cellNumber;
            
           // private MazeCell[] cellNeighbours;
            private bool isATrap;
            private int mCoordinate;
            private int nCoordinate;

            public int _mCoordinate
            {
                get => mCoordinate;
                set => mCoordinate = value;
            }
            public int _nCoordinate
            {
                get => nCoordinate;
                set => nCoordinate = value;
            }
            public bool Trap
            {
                get => isATrap;
                set => isATrap = value;
            }
        }

        


    }

    
}
