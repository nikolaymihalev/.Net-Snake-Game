﻿using Snake_Game.Enums;
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

        public void ChangeDirection(Direction dir) 
        {
            Dir = dir;
        }

        public void Move() 
        {
            Position headPos = HeadPosition().Translate(Dir);
            GridValue hit = WillHit(headPos);

            if (hit == GridValue.Outside || hit == GridValue.Snake) 
            {
                GameOver = true;
            }
            else if (hit == GridValue.Empty)
            {
                RemoveTail();
                AddHead(headPos);
            }
            else if (hit == GridValue.Food)
            {
                AddHead(headPos);
                Score++;
                AddFood();
            }
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

        void RemoveTail() 
        {
            Position tail = snakePositions.Last.Value;
            Grid[tail.Row, tail.Col] = GridValue.Empty;
            snakePositions.RemoveLast();
        }

        bool OutsideGrid(Position pos) 
        {
            return pos.Row < 0 || pos.Row >= Rows || pos.Col < 0 || pos.Col >= Cols;
        }

        GridValue WillHit(Position headPos) 
        {
            if (OutsideGrid(headPos)) 
            {
                return GridValue.Outside;
            }
            
            if (headPos== TailPosition()) 
            {
                return GridValue.Empty;
            }

            return Grid[headPos.Row, headPos.Col];
        }
    }
}
