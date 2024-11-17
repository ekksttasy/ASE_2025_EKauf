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
        public Circle(Color color, int x, int y, int radius) : base(color, x, y)
        {
            this.radius = radius;
        }

        public void Draw(Graphics g, Pen pen, bool filled)
        {
            g.DrawEllipse(pen, this.x, this.y, (this.radius*2), (this.radius*2));
        }
    }
}
