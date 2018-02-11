using System;
using System.Collections.Generic;
using System.Text;

namespace WpfDodgeGame
{
    class Tool
    {
        protected String _name;
        protected int _x;//width
        protected int _y;//height

        public Tool(int x, int y)
        {
            this._name = "Basic Tool";
            this._x = x;
            this._y = y;
        }
        //do nothing 
        public virtual void Move(char c,int row, int col, int heightBorder, int widthBorder)
        {
            
            
        }
        //returns tool location x=colum y=row
        public int[] GetLocation()
        {
            int[] temp = { this._x, this._y };
            return temp;
        }
        //return tool name/type
        public override string ToString()
        {
            return this._name;
        }
    }
}
