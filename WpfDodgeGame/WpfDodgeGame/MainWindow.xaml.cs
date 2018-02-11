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

namespace WpfDodgeGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


        /*
         * problems:
         * defining a generic path for the files in the project
         * loading a saved game
         * 
         */

    public partial class MainWindow : Window
    {
        Game _dodgeGame;
        string _gameState;
        const int INSTANCES_SIZE = 20;

        public MainWindow()
        {
            InitializeComponent();
        }
        /*
         * 
         * start a new game
         * 
         */
        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            this._dodgeGame = new Game((((int)Cnvs.ActualHeight)/ INSTANCES_SIZE), (((int)Cnvs.ActualWidth)/ INSTANCES_SIZE));
            this._dodgeGame.NewGame();
            this._gameState = this._dodgeGame.ToString();
            UpdateGameState();
            StartBtn.IsEnabled = false;
        }
        /*
         * 
         * restart game
         * 
         */
        private void NewGameBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!StartBtn.IsEnabled)
            {
                this._dodgeGame.NewGame();
                UpdateGameState();
            }
            
        }
        /*
         * 
         * pause/resume game
         * 
         */
        private void PauseBtn_Click(object sender, RoutedEventArgs e)
        {
            if(!StartBtn.IsEnabled)
            {
                this._gameState = this._dodgeGame.PauseStart();
                if (this._gameState.Equals("Pause"))
                    PauseBtn.Content = "Start";
                else
                    PauseBtn.Content = "Pause";
            }
        }

        /*
         * suppose to load a saved game
         * not working, not sure which assumption as for the input should i take, could generally use System.IO.File.ReadAllText
         * and use loop to build into the string matrix format
         */
        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {/*
            if (!StartBtn.IsEnabled)
            {
                string[,] savedGame = new string[this._dodgeGame.GameState().GetLength(0), this._dodgeGame.GameState().GetLength(1)];

                //updatesavedgame

                this._dodgeGame.Load(savedGame);
                UpdateGameState();
            }*/
            
        }
        /*
         * updates the board according to the game state
         * 
         * 
         */
        private void UpdateGameState()
        {
            
            this._gameState = this._dodgeGame.ToString();
            GameStateTextBlock.Text = this._gameState;
            if (this._gameState.Equals("Playing"))
            {
                string[,] boardRepresentation = this._dodgeGame.GameState();
                Cnvs.Children.Clear();
                for (int ind = 0; ind < boardRepresentation.GetLength(0); ind++)
                {
                    if (boardRepresentation[ind, 0] != null && (!boardRepresentation[ind, 0].Equals("")))
                    {
                        Image tmp = new Image();
                        tmp.Source = new BitmapImage(new Uri("C:/Users/Tomer/Desktop/WpfDodgeGame/WpfDodgeGame/Assets/" + boardRepresentation[ind,0] + ".png"));
                        tmp.Height = INSTANCES_SIZE;
                        tmp.Width = INSTANCES_SIZE;
                        Canvas.SetLeft(tmp, (((int.Parse(boardRepresentation[ind, 1])+1)* INSTANCES_SIZE)));
                        Canvas.SetTop(tmp, (((int.Parse(boardRepresentation[ind, 2]) + 1) * INSTANCES_SIZE)));
                        Cnvs.Children.Add(tmp);
                    }

                }
            }
            
        }
        /*
         * saves the game to text file
         * 
         */
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!StartBtn.IsEnabled)
            {
                string[,] arrToSave = this._dodgeGame.GameState();
                string toSave = "";
                for(int ind = 0; ind<arrToSave.GetLength(0); ind++)
                {
                    for(int innerInd = 0; innerInd < arrToSave.GetLength(1);innerInd++)
                    {
                        toSave = toSave + arrToSave[ind, innerInd] + " ";
                    }
                }
                System.IO.File.WriteAllText("C:/SavedGame.txt", toSave);
            }



        }
        /*
         * allow playing the game according to arrow key pressed
         * 
         */
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                this._dodgeGame.Move('d');
                this._dodgeGame.Move();
            }
            else
            {
                if (e.Key == Key.Down)
                {
                    this._dodgeGame.Move('u');
                    this._dodgeGame.Move();
                }
                else
                {
                    if (e.Key == Key.Left)
                    {
                        this._dodgeGame.Move('r');
                        this._dodgeGame.Move();
                    }
                    else
                    {
                        if (e.Key == Key.Right)
                        {
                            this._dodgeGame.Move('l');
                            this._dodgeGame.Move();
                        }
                    }
                }
            }

            UpdateGameState();


        }
    }
}
