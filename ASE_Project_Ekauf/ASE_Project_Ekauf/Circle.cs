using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOOSE;

namespace ASE_Project_Ekauf
{
    /// <summary>
    /// Circle constructor to draw a perfect circle.
    /// </summary>
    internal class Circle : Shape
    {
        protected int radius; // radius of circle to be drawn
        /// <summary>
        /// Constructor for creating perfect circles.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        public Circle(Color color, int x, int y, int radius) : base(color, x, y)
        {
            this.radius = radius;
        }

        /// <summary>
        /// Draw method writes a created circle to graphics object.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        /// <param name="filled"></param>
        new public void Draw(Graphics g, Pen pen, bool filled)
        {
            g.DrawEllipse(pen, this.x, this.y, (this.radius*2), (this.radius*2));
        }
    }
}
