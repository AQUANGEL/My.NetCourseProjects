using System;
using System.Collections.Generic;
using System.Text;

namespace WpfDodgeGame
{
    class Enemy:Tool
    {
        public Enemy(int x, int y) : base(x,y)
        {
            base._name = "Enemy";
        }
        /*
         * gets player location and move towards it
         * 
         * 
         */
        public override void Move(char c, int row, int col, int heightBorder, int widthBorder)
        {
            
            if (base._y < row)
                base._y++;
            else
            {
                if(base._y > row)
                    base._y--;
            }
                
            if (base._x < col)
                base._x++;
            else
            {
                if (base._x > col)
                    base._x--;
            }
                

            
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
