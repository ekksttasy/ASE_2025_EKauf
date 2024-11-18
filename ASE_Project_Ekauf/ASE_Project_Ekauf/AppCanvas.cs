using BOOSE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Project_Ekauf
{
    /// <summary>
    /// Class implements ICanvas from Boose to render Boose code on form.
    /// </summary>
    
    public class AppCanvas : Canvas, ICanvas
    {
        public Pen myPen;
        private Bitmap bitmap;
        private int x_Pos = 0;
        private int y_Pos = 0;
        private Color background_color;
        private object pColor;

        private static int mapX = 1080;
        private static int mapY = 1080;

        Font defaultFont = new Font("Arial", 16, FontStyle.Bold);

        /// <summary>
        /// Constructor for new AppCanvas
        /// </summary>
        /// <param name="background_color"></param>
        public AppCanvas(Color background_color)
        {
            this.background_color = background_color;
            bitmap = new Bitmap(mapX, mapY);
            pColor = Color.FromArgb(0, 0, 0);
            myPen = new Pen((Color)pColor, 3);
            Clear();
        }

        /// <summary>
        /// Object form of pen color. 
        /// </summary>
        public override object PenColour
        {
            //retrieve/change pen color
            get;
            set;
        }

        /// <summary>
        /// Changes pen color using RGB integer values. Must be between 0 and 255 for a valid color.
        /// </summary>
        /// <param name="red">The integer representing red. </param>
        /// <param name="green">The integer representing green. </param>
        /// <param name="blue">The integer representing blue. </param>
        public override void SetColour(int red, int green, int blue)
        {
            pColor = Color.FromArgb(red, green, blue);
            myPen.Color  = (Color)pColor;
            Debug.WriteLine($"Color is set to RGB {pColor}");
        }

        /// <summary>
        /// The X Position of the pen.
        /// </summary>
        public override int Xpos
        {
            get;
            set;
        }

        /// <summary>
        /// The Y Position of the pen.
        /// </summary>
        public override int Ypos
        {
            get;
            set;
        }

        /// <summary>
        /// Resets bitmap image to the default background color.
        /// </summary>
        public override void Clear()
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(background_color);
            }

        }

        /// <summary>
        /// Returns the pen to the starting position of (0,0) by changing the values of x_Pos and y_Pos.
        /// </summary>
        public override void Reset()
        {
            x_Pos = 0;
            y_Pos = 0;
        }

        /// <summary>
        /// Sets the display size of the bitmap output. This only changes the bitmap and not the size of the picture box.
        /// 
        /// If the picture box size is smaller than the new bitmap size, not all elements may be visible.
        /// </summary>
        /// <param name="width">The new x value defining the width of the bitmap. </param>
        /// <param name="height">The new y value defining the height of the bitmap. </param>
        public override void Set(int width, int height)
        {
            int mapX = width;
            int mapY = height;
        }

        /// <summary>
        /// Draws a line from the pen's position to a given point.
        /// </summary>
        /// <param name="x">The x value of the point to be drawn to. </param>
        /// <param name="y">The y value of the point to be drawn to. </param>
        public override void DrawTo(int x, int y)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawLine(myPen, x_Pos, y_Pos, x, y);
            }
            x_Pos = x;
            y_Pos = y;
        }

        /// <summary>
        /// Moves the pen to a given point.
        /// </summary>
        /// <param name="x">The x value of the point to be moved to. </param>
        /// <param name="y">The y value of the point to be moved to. </param>
        public override void MoveTo(int x, int y)
        {
            x_Pos = x;
            y_Pos = y;
        }

        /// <summary>
        /// Writes a string of text to the bitmap from the current position of the pen.
        /// </summary>
        /// <param name="text">The string of text to be written. </param>
        public void WriteText(string text)
        {
            Brush brush = new SolidBrush((Color)pColor);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawString(text, defaultFont, brush, x_Pos, y_Pos);
            }
        }

        /// <summary>
        /// Calls the Draw method from the Circle class to draw a circle to the bitmap from the current pen position.
        /// </summary>
        /// <param name="radius">The radius of the circle to be drawn. </param>
        /// <param name="filled">Flag marking whether the circle is to be drawn filled or not. True = filled, False = empty. </param>
        public override void Circle(int radius, bool filled)
        {
            Circle myCircle = new Circle((Color)pColor, x_Pos, y_Pos, radius);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                myCircle.Draw(g, myPen, filled);
            }
        }

        /// <summary>
        /// Calls the Draw method from the Rect class to draw a rectangle to the bitmap from the current pen position. 
        /// </summary>
        /// <param name="width">The x length of the rectangle to be drawn. </param>
        /// <param name="height">The y length of the rectangle to be drawn. </param>
        /// <param name="filled">Flag marking whether the rectangle is to be drawn filled or not. True = filled, False = empty. </param>
        public override void Rect(int width, int height, bool filled)
        {
            Rect myRect = new Rect((Color)pColor, x_Pos, y_Pos, width, height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                myRect.Draw(g, myPen, filled);
            }
        }

        /// <summary>
        /// Calls the Draw method from the Triangle class to draw a triangle to the bitmap from the current pen position.
        /// </summary>
        /// <param name="width">The x length of the base of the triangle to be drawn. </param>
        /// <param name="height">The y length of the triangle to be drawn. </param>
        public override void Tri(int width, int height)
        {
            Debug.WriteLine("Triangle method not yet implemented");
        }

        /// <summary>
        /// Retrieves the bitmap graphics object to be drawn on.
        /// </summary>
        /// <returns>'bitmap' - The bitmap to be used in drawing methods. </returns>
        public override object getBitmap()
        {
            return bitmap;
        }
    }
}
