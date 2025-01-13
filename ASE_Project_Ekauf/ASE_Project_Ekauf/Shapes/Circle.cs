using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOOSE;

namespace ASE_Project_Ekauf.Shapes
{
    /// <summary>
    /// Circle class to define new circle objects.
    /// </summary>
    internal class Circle : Shape
    {
        protected int radius; // radius of circle to be created
        /// <summary>
        /// Constructor for creating perfect circles.
        /// </summary>
        /// <param name="color">The color of the inside of the circle. </param>
        /// <param name="x">The starting x point of the circle. </param>
        /// <param name="y">The starting y point of the circle. </param>
        /// <param name="radius">The radius of the circle (length from the central point). </param>
        public Circle(Color color, int x, int y, int radius) : base(color, x, y)
        {
            this.radius = radius;
        }

        /// <summary>
        /// Draw method writes a created circle to graphics object.
        /// </summary>
        /// <param name="g">The graphics object the circle is drawn to. </param>
        /// <param name="pen">The pen used to draw the circle with. </param>
        /// <param name="filled">Flag to mark whether the circle is filled or not. True = filled, False = empty. </param>
        new public void Draw(Graphics g, Pen pen, bool filled)
        {
            g.DrawEllipse(pen, x, y, radius * 2, radius * 2);
        }
    }
}
