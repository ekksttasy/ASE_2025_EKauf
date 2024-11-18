using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOOSE;

namespace ASE_Project_Ekauf
{
    /// <summary>
    /// Parent class to all writeable shape objects.
    /// </summary>

    abstract class Shape : Object
    {
        protected Color color; // color var protected
        protected int x, y; // position var protected 
        /// <summary>
        /// Constructor for shape objects.
        /// </summary>
        /// <param name="=color"></param>
        public Shape(Color color, int x, int y)
        {
            this.color = color;
            this.x = x;
            this.y = y;
        }

        protected void Draw(Graphics g, Pen pen, bool filled)
        {
            throw new NotImplementedException();
        }

    }
}
