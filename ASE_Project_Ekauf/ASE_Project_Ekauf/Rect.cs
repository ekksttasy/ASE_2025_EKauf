using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOOSE;

namespace ASE_Project_Ekauf
{
    /// <summary>
    /// Rectangle class to draw rectangles or squares.
    /// </summary>
    internal class Rect : Shape
    {
        protected int width;
        protected int height;
        /// <summary>
        /// Constructor for creating rectangles.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Rect(Color color, int x, int y, int width, int height) : base(color, x, y)
        {
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Draw method writes a created circle to graphics object.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        /// <param name="filled"></param>
        public void Draw(Graphics g, Pen pen, bool filled)
        {
            g.DrawRectangle(pen, x, y, width, height);
        }
    }
}
