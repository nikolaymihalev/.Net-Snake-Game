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

        public MainWindow()
        {
            InitializeComponent();
            gridImages = SetupGrid();
            gameState = new GameState(rows, cols);
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
    }
}
