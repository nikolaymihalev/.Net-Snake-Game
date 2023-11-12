using Snake_Game.Enums;
using System;
using System.Collections.Generic;

namespace Snake_Game.Models
{
    public class GameState
    {
        readonly LinkedList<Position> snakePositions = new LinkedList<Position>();
        readonly Random random = new Random();

        public int Rows { get; }
        public int Cols { get; }
        public GridValue[,] Grid { get; }
        public Direction Dir { get; private set; }
        public int Score { get; private set; }
        public bool GameOver { get; private set; }

        public GameState(int rows,int cols)
        {
            Rows = rows;
            Cols = cols;
            Grid = new GridValue[Rows,Cols];
            Dir = Direction.Right;
        }

    }
}
