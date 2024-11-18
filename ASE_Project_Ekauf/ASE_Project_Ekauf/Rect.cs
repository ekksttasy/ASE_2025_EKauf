using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOOSE;

namespace ASE_Project_Ekauf
{
    /// <summary>
    /// Rectangle class to define new rectangle objects.
    /// </summary>
    internal class Rect : Shape
    {
        protected int width; //width of rectangle to be created
        protected int height; //height of rectangle to be created

        /// <summary>
        /// Constructor for creating new rectangle objects.
        /// </summary>
        /// <param name="color">The color of the inside of the rectangle. </param>
        /// <param name="x">The starting x position to draw the rectangle from. </param>
        /// <param name="y">The starting y position to draw the rectangle from. </param>
        /// <param name="width">The x length of the rectangle. </param>
        /// <param name="height">The y length of the rectangle. </param>
        public Rect(Color color, int x, int y, int width, int height) : base(color, x, y)
        {
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Draw method writes a created rectangle to the specified graphics object.
        /// </summary>
        /// <param name="g">The graphics object to draw the rectangle to. </param>
        /// <param name="pen">The pen to draw the rectangle with. </param>
        /// <param name="filled">Flag to mark whether the rectangle is filled or not. True = filled, False = empty. </param>
        new public void Draw(Graphics g, Pen pen, bool filled)
        {
            g.DrawRectangle(pen, x, y, width, height);
        }
    }
}
