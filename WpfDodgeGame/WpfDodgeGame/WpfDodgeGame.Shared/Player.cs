using System;
using System.Collections.Generic;
using System.Text;

namespace WpfDodgeGame
{
    class Player:Tool
    {
        public Player(int x, int y) : base( x,  y)
        {
            base._name = "Player";
        }
        /*
         * gets char and borders and make a move accordingly
         * 
         */
        public override void Move(char c, int row, int col, int heightBorder, int widthBorder)
        {
            
            if (c == 'u')
            {
                if (base._y < heightBorder)
                    base._y++;
                else
                    base._y = 0;
            }
            else
            {
                if (c == 'd')
                {
                    if (base._y > 0)
                        base._y--;
                    else
                        base._y = heightBorder;
                }
                else
                {
                    if (c == 'l')
                    {
                        if (base._x < widthBorder)
                            base._x++;
                        else
                            base._x = 0;
                    }
                    else
                    {
                        if (c == 'r')
                        {
                            if (base._x > 0)
                                base._x--;
                            else
                                base._x = widthBorder;
                        }
                    }
                }
            }

            
        }

        public override string ToString()
        {
            return base._name;
        }
    }
}
