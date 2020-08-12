using System;
using System.Collections.Generic;
using System.Text;


namespace BlindMaze
{
    public class Player
    {
        public int playerLives = 3;
        public int[,] currentPosition = new int[1, 1];
        public int mPosition;
        public int nPosition;
        public bool canMoveRight;
        public bool canMoveLeft;
        public bool canMoveDown;
        public bool canMoveUp;

        public void CheckPossibleMovements(Player currentPlayer, Maze currentMaze)
        {
            currentPlayer.canMoveRight = false;
            currentPlayer.canMoveLeft = false;
            currentPlayer.canMoveUp = false;
            currentPlayer.canMoveDown = false;

            if (currentPlayer.mPosition + 1 <= currentMaze.mDimension - 1)
                currentPlayer.canMoveDown = true;         
            if (currentPlayer.mPosition - 1 >= 0)
                currentPlayer.canMoveUp = true;
            if (currentPlayer.nPosition + 1 <= currentMaze.nDimension - 1)
                currentPlayer.canMoveRight = true;
            if (currentPlayer.nPosition - 1 >= 0)
                currentPlayer.canMoveLeft = true;

        }

    }
   
}
