using Snake_Game.Enums;
using Snake_Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Snake_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly Dictionary<GridValue, ImageSource> gridValToImage = new()
        {
            { GridValue.Empty, Images.Empty },
            { GridValue.Snake, Images.Body },
            { GridValue.Food, Images.Food }
        };


        readonly int rows = 15;
        readonly int cols = 15;
        readonly Image[,] gridImages;
        GameState gameState;
        bool gameRunning;

        public MainWindow()
        {
            InitializeComponent();
            gridImages = SetupGrid();
            gameState = new GameState(rows, cols);
        }

        private async Task RunGame()
        {
            Draw();
            Overlay.Visibility = Visibility.Hidden;
            await GameLoop();
        }

        private async void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Overlay.Visibility == Visibility.Visible) 
            {
                e.Handled = true;
            }
            if (!gameRunning) 
            {
                gameRunning = true;
                await RunGame();
                gameRunning = false;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) 
        {
            if (gameState.GameOver) 
            {
                return;
            }
            switch (e.Key) 
            {
                case Key.A:
                    gameState.ChangeDirection(Direction.Left);
                    break;
                case Key.S:
                    gameState.ChangeDirection(Direction.Down);
                    break;
                case Key.D:
                    gameState.ChangeDirection(Direction.Right);
                    break;
                case Key.W:
                    gameState.ChangeDirection(Direction.Up);
                    break;
            }
        }

        Image[,] SetupGrid() 
        {
            Image[,] images=new Image[rows, cols];
            GameGrid.Rows = rows;
            GameGrid.Columns = cols;

            for (int i = 0; i < rows; i++) 
            {
                for (int j = 0; j < cols; j++)
                {
                    Image image = new Image
                    {
                        Source = Images.Empty
                    };
                    images[i, j] = image;
                    GameGrid.Children.Add(image);
                }
            }
            return images;
        }

        void Draw() 
        {
            DrawGrid();
            ScoreText.Text = $"SCORE {gameState.Score}";
        }

        void DrawGrid() 
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    GridValue gridValue = gameState.Grid[i, j];
                    gridImages[i, j].Source = gridValToImage[gridValue];
                }
            }
        }

        private async Task GameLoop()
        {
            while (!gameState.GameOver)
            {
                await Task.Delay(100);
                gameState.Move();
                Draw();
            }
        }
    }
}
