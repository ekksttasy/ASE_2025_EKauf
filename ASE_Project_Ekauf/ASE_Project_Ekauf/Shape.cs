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
        /// Base constructor inherited by shape classes to create a new shape.
        /// </summary>
        /// <param name="color">The color of the inside of the shape when filled. </param>
        /// <param name="x">The x value of the starting point of the shape. </param>
        /// <param name="y">The y value of the starting point of the shape. </param>
        public Shape(Color color, int x, int y)
        {
            this.color = color;
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Base draw method to write a shape to a graphics object.
        /// </summary>
        /// <param name="g">The graphics object the shape is drawn to. </param>
        /// <param name="pen">The pen used to draw the shape with. </param>
        /// <param name="filled">Flag to mark whether the shape is filled or not. True = filled, False = empty. </param>
        /// <exception cref="NotImplementedException">Method is intended to be overriden by classes further down the hierarchy. </exception>
        protected void Draw(Graphics g, Pen pen, bool filled)
        {
            throw new NotImplementedException();
        }

    }
}
