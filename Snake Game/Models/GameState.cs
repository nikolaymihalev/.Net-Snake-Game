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
            AddSnake();
            AddFood();
        }

        public Position HeadPosition() 
        {
            return snakePositions.First.Value;
        }
        
        public Position TailPosition() 
        {
            return snakePositions.Last.Value;
        }

        public IEnumerable<Position> SnakePositions() 
        {
            return snakePositions;
        }

        void AddSnake() 
        {
            int r = Rows / 2;
            for(int c = 1; c <= 3; c++) 
            {
                Grid[r, c] = GridValue.Snake;
                snakePositions.AddFirst(new Position(r, c));
            }
        }

        IEnumerable<Position> EmptyPositions() 
        {
            for (int r = 0; r < Rows; r++) 
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (Grid[r, c] == GridValue.Empty)
                        yield return new Position(r, c);
                }
            }
        }

        void AddFood() 
        {
            List<Position> empty = new List<Position>(EmptyPositions());
            if (empty.Count == 0) 
            {
                return;
            }
            Position pos = empty[random.Next(empty.Count)];
            Grid[pos.Row, pos.Col] = GridValue.Food;
        }

        void AddHead(Position pos) 
        {
            snakePositions.AddFirst(pos);
            Grid[pos.Row, pos.Col] = GridValue.Snake;
        }
    }
}
