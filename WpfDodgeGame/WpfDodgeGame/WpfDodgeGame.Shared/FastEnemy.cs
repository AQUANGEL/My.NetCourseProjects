using System;
using System.Collections.Generic;
using System.Text;

namespace WpfDodgeGame
{
    class FastEnemy:Tool
    {

        public FastEnemy(int x, int y) : base(x,y)
        {
            base._name = "FastEnemy";
        }
        /*
         * gets player location and move towards it
         * 
         * 
         */
        public override void Move(char c, int row, int col, int heightBorder, int widthBorder)
        {
            
            if (base._y < row)
            {
                base._y += 2;
                if (base._y > heightBorder)
                    base._y = 0;
            }
            else
            {
                base._y -= 2;
                if (base._y <0)
                    base._y = heightBorder;
            }
                
            if (base._x < col)
            {
                base._x += 2;
                if (base._x > widthBorder)
                    base._x = 0;
            }   
            else
            {
                base._x -= 2;
                if (base._x < 0)
                    base._x = widthBorder;
            }
                

            
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
