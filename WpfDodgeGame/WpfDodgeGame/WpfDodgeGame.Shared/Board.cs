using System;
using System.Collections.Generic;
using System.Text;

namespace WpfDodgeGame
{
    class Board
    {
        private Tool[] _toolsBoard;
        private Player _player;
        //private Tool _temp;
        private int _height;
        private int _width;
        private Random _rnd;
        private int _numOfEnemies;

        public Board(int boardHeight, int boardWidth, int numOfEnemies = 10)
        {
            this._height = boardHeight;
            this._width = boardWidth;
            this._rnd = new Random();
            this._numOfEnemies = numOfEnemies;
            SetBoard();
            //this._temp = null;
        }
        /*
         * sets the basic board
         * with 1 player and 2 types of enemies
         * 
         */
        public void SetBoard()
        {
            //set player and enemies to board randomly
            ClearBoard();
            int row, col;
            int ind = 0;
            row = this._rnd.Next(this._height);
            col = this._rnd.Next(this._width);
            this._player= new Player(col, row);
            
            int innerInd;
            int[] pastLoc,playerLoc = this._player.GetLocation();
            bool isOnOcuppiedLoc;



            while(ind<this._toolsBoard.Length)
            {
                isOnOcuppiedLoc = true;
                innerInd = 0;
                while(isOnOcuppiedLoc)
                {
                    if((col == playerLoc[0] && row == playerLoc[1]))
                    {
                        row = this._rnd.Next(this._height);
                        col = this._rnd.Next(this._width);
                    }
                    else
                    {
                        if(innerInd<ind)
                        {
                            pastLoc = this._toolsBoard[innerInd].GetLocation();
                            if ((col == pastLoc[0] && row == pastLoc[1]) )
                            {
                                row = this._rnd.Next(this._height);
                                col = this._rnd.Next(this._width);
                                innerInd = 0;
                            }
                            else
                            {
                                innerInd++;
                            }
                        }
                        else
                        {
                            isOnOcuppiedLoc = false;
                        }
                    }
                }

                if(ind%2==0)
                {
                    this._toolsBoard[ind] = new Enemy(col, row);
                }
                else
                {
                    this._toolsBoard[ind] = new FastEnemy(col, row);
                }

                
                ind++;
            }
            
        }
        /*
         * sets the board state from a string matrix
         * gets a string matrix represented as:
         * tooltype , x , y
         */
        public void SetBoard(string[,] str)
        {
            //scan char array to set players and enemies on board
            this._toolsBoard = new Tool[str.GetLength(0)-1];
            for(int ind = 0, toolsInd = 0; ind< str.GetLength(0); ind++)
            {
                if(str[ind,0].Equals("Enemy"))
                {
                    this._toolsBoard[toolsInd] = new Enemy(int.Parse(str[ind, 1]), int.Parse(str[ind, 2]));
                    toolsInd++;
                }
                else
                {
                    if (str[ind, 0].Equals("FastEnemy"))
                    {
                        this._toolsBoard[toolsInd] = new FastEnemy(int.Parse(str[ind, 1]), int.Parse(str[ind, 2]));
                        toolsInd++;
                    }
                    else
                    {
                        if (str[ind, 0].Equals("Player"))
                        {
                            this._player = new Player(int.Parse(str[ind, 1]), int.Parse(str[ind, 2]));
                        }
                    }
                }
            }

        }
        /*
         * sets a clear/empety board
         * 
         */
        private void ClearBoard()
        {
            this._toolsBoard = new Tool[this._numOfEnemies];
        }
        /*
         * return a matrix representaion of the board state
         * tool type , x , y
         * 
         */
        public string[,] GetBoard()
        {
            string[,] str = new string[this._toolsBoard.Length+1, 3];
            int[] loc;
            //scan the tools array for tool type and location
            int ind = 0;
            if(this._player!=null)
            {
                str[ind, 0] = this._player.ToString();
                loc = this._player.GetLocation();
                str[ind, 1] = loc[0].ToString();
                str[ind, 2] = loc[1].ToString();
            }
            

            for (ind = 1; ind< this._toolsBoard.Length; ind++)
            {
                if(this._toolsBoard[ind]!=null)
                {
                    str[ind, 0] = this._toolsBoard[ind].ToString();
                    loc = this._toolsBoard[ind].GetLocation();
                    str[ind, 1] = loc[0].ToString();
                    str[ind, 2] = loc[1].ToString();
                }
                
            }

            return str;
        }
        /*
         * לשפר את הפולימורפיזם
         * הבעיה:
         * מיקום השחקן
         * פתרונות:
         * להפריד את השחקן מהאויבים - ואז מה הטעם בTOOL?
         * להניח שהשחקן ראשון ואז נשאל האם לבדוק את זה בהגדרת המערך בעת LOAD
         * להשתמש במערך TOOL לסוגים שונים של אויבים  וכלים?
         * 
         */

        /*
         * moves the player
         * gets a char for the move direction
         * returns true if player got hit
         * 
         */
        public bool Move(char c)
        {
            if(this._player!=null)
            {
                this._player.Move(c, 0, 0, this._height, this._width);
                for (int ind = 0; ind < this._toolsBoard.Length; ind++)
                {
                    if(this._toolsBoard[ind]!=null)
                    {
                        if (IsClashed(this._player.GetLocation(), this._toolsBoard[ind].GetLocation()))
                        {
                            this._player = null;
                            this._toolsBoard[ind] = null;
                            return true;
                        }
                    }
                    
                }
            }
            
            return false;
        }

        /*
         * moves the enemies
         * 
         * returns true if player got hit
         * erase clashed tools
         */
        public bool Move()
        {
            int[] playerLoc = this._player.GetLocation();

            for(int ind = 0, innerInd; ind < this._toolsBoard.Length; ind++)
            {
                if(this._toolsBoard[ind]!=null)
                {
                    this._toolsBoard[ind].Move('0', playerLoc[1], playerLoc[0], this._height, this._width);
                    if (IsClashed(playerLoc, this._toolsBoard[ind].GetLocation()))
                    {
                        this._player = null;
                        this._toolsBoard[ind] = null;
                        return true;
                    }
                    else
                    {
                        for (innerInd = 0; innerInd < ind; innerInd++)
                        {
                            if(this._toolsBoard[innerInd] !=null&& this._toolsBoard[ind] != null)
                            {
                                if (IsClashed(this._toolsBoard[innerInd].GetLocation(), this._toolsBoard[ind].GetLocation()))
                                {
                                    this._toolsBoard[ind] = null;
                                    this._toolsBoard[innerInd] = null;
                                }
                            }
                            
                        }
                    }
                }
                


            }
            return false;
        }
        /*
         * checks the num of enemies left on the board
         * 
         */
        public int EnemiesLeft()
        {
            int counter = 0;
            for( int ind = 0; ind< this._toolsBoard.Length; ind++)
            {
                if (this._toolsBoard[ind] != null)
                    counter++;
            }
            return counter;
        }

        /*
         * check colisions
         * gets 2 tools locations
         * return true if clashed
         * 
         */
        private bool IsClashed(int[] loc1, int[] loc2)
        {
            if (loc1[0] == loc2[0] && loc1[1] == loc2[1])
                return true;
            return false;
        }
    }
}
