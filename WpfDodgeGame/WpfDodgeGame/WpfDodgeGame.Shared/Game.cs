using System;
using System.Collections.Generic;
using System.Text;

namespace WpfDodgeGame
{

    class Game
    {
        private Board _b;
        private string _gameSituation;
        private bool _pause;
        private bool _gameOver;
        //private DateTime _dt;

        public Game(int boardheight, int boardWidth)
        {
            this._b = new Board(boardheight, boardWidth);
            this._gameSituation = "Playing";
            this._pause = false;
            this._gameOver = false;
            //set new datetime?
        }

        public void NewGame()
        {
            this._b.SetBoard();
            this._gameSituation = "Playing";
            this._pause = false;
            this._gameOver = false;
        }
        /*
         * 
         * gets a char(u/d/l/r) to move the player in the char direction
         * if wrong char wont move
         * changes the game state string accordingly
         */
        public void Move(char c)
        {
            if((!this._pause) && (!this._gameOver))
            {
                if (this._b.Move(c))
                {
                    this._gameSituation = "You Lose";
                    this._gameOver = true;
                }
                else
                {
                    if (_b.EnemiesLeft() <= 1)
                    {
                        this._gameSituation = "You Won";
                        this._gameOver = true;
                    }
                }
            }
            
        }
        /*
         * moves the enemies
         * change the game state accordingly
         * 
         */
        public void Move()
        {
            if((!this._pause)&&(!this._gameOver))
            {
                if (this._b.Move())
                {
                    this._gameSituation = "You Lose";
                    this._gameOver = true;
                }
                else
                {
                    if (_b.EnemiesLeft() <= 1)
                    {
                        this._gameSituation = "You Won";
                        this._gameOver = true;
                    }
                }
            }
        }
            
            
        
        /*
         * returns a string matrix that represents the board
         * 
         */
        public string[,] GameState() //save or update game state
        {

            return this._b.GetBoard(); //return the board state to char matrix [10,3] = [tooltype, tool row, tool colum ]?
        }
        /*
         * gets a string matrix that represents the board to set the board
         * 
         */
        public void Load(string[,] str)
        {
            this._b.SetBoard(str);
        }
        
        public string PauseStart()
        {
            this._pause = !this._pause;
            if (this._pause)
                return "Pause";
            return ToString();
        }

         /*
          * returns the game situation string win/lose/playing
          * 
          */
        public override string ToString()
        {
            return this._gameSituation;
        }
    }
}

